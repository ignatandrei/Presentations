import fs from 'fs';
//npx genaiscript run befCommit     --model "ollama:phi3.5"
//model: "openai:gpt-4o",
//model:"ollama:phi3.5",
//model:"ollama:llama3.3",
//model:"ollama:gemma2:27b",
//model:"mistral",
//model: "transformers:onnx-community/Qwen2.5-Coder-0.5B-Instruct:q4",
// Get the model name from CLI arguments or use a default value
console.log("test");    

const args = process.argv.slice(2);
const modelArgIndex = args.indexOf("--model");
const modelName = modelArgIndex !== -1 ? args[modelArgIndex + 1] : "ollama:gemma2:27b";
console.log("Model name: " + modelName);
script({
    title: "git commit message",
    description: "Generate a commit message for all staged changes",
    model: modelName,
    system: ["system"],
})

//https://microsoft.github.io/genaiscript/guides/auto-git-commit-message/
//https://github.com/microsoft/genaiscript/blob/main/packages/sample/genaisrc/samples/gcm.genai.mts


//let { stdout } = await host.exec("git", ["diff", "--cached"])
// Check for staged changes and stage all changes if none are staged
const diff = await git.diff({
    staged: true,
    askStageOnEmpty: true,
    llmify:true
})

if (!diff) cancel("no staged changes")
console.log(diff)

// chunk if case of massive diff
const chunks = await tokenizers.chunk(diff, { chunkSize: 10000 })
if (chunks.length > 1)
    console.log(`staged changes chunked into ${chunks.length} parts`)
//- <date> should be the today date in the format YYYY-MM-DD
const dateAll = new Date().toISOString().split("T")
const date = dateAll[0]
const time = dateAll[1]
const commonMessage =`        
        
For each file that you find in the diff, generate a commit message in the following format:

        <type>(<file>) :  <description>

        - <file> should be the file path relative to the repository root
        - <type> can be one of the following: feat, fix, docs, style, refactor, perf, test, build, ci, chore, revert
        - <description> is a short, imperative present-tense description of the change        
        - do NOT use markdown syntax
        - do NOT add quotes or code blocks
        - do NOT use gitmoji        
        - keep it short, 1 line only, maximum 50 characters
        - follow the conventional commit spec at https://www.conventionalcommits.org/en/v1.0.0/#specification
        - do NOT confuse delete lines starting with '-' and add lines starting with '+'        
        - do NOT respond anything else than the commit message
        - If you are NOT sure of the file, do not mention it in the commit message
        
 `
let choice
let message = "---------------------------------\n"
let messageSummary
do {
    // Generate a conventional commit message based on the staged changes diff
    message = ""
    for (const chunk of chunks) {
        const res = await runPrompt(
            (_) => {
                _.def("GIT_DIFF", chunk, {
                    maxTokens: 10000,
                    language: "diff",
                    detectPromptInjection: "available",
                })
                _.$`
                Generate a git conventional commit message that summarizes the changes in GIT_DIFF.

                GIT_DIFF:
                    ${commonMessage}
        `
            },
            {
                model: modelName,//"large", // Specifies the LLM model to use for message generation
                label: "generate commit message", // Label for the prompt task
                system: [
                    "system.assistant",
                    "system.safety_jailbreak",
                    "system.safety_harmful_content",
                    "system.safety_validate_harmful_content",
                ],
            }
        )
        if (res.error) throw res.error
        message += res.text + "\n"
        message += "---------------------------------\n"
    }

    // since we've concatenated the chunks, let's compress it back into a single sentence again
    //if (chunks.length > 1) 
    {
        const res =
            await prompt`Generate a git conventional commit message that summarizes the COMMIT_MESSAGES. 
       
        Instructions:
        - generate a short, 1 line only,max 30 characters imperative present-tense description of the change        
        - do NOT use markdown syntax
        - do NOT add quotes or code blocks
        - can use gitmoji        
        - do NOT put file names or paths in the description
        - the result should be different from the messages 
        
        COMMIT_MESSAGES:
        ${message}
        `.options({
                model: modelName,//"large",
                label: "summarize chunk commit messages",
                system: [
                    "system.assistant",
                    "system.safety_jailbreak",
                    "system.safety_harmful_content",
                    "system.safety_validate_harmful_content",
                ],
            })
        if (res.error) throw res.error
        messageSummary = res.text
    }

    message = message?.trim()
    if (!message) {
        console.log(
            "No commit message generated, did you configure the LLM model?"
        )
        break
    }
    console.log("Summary : "+ messageSummary);
    console.log("Message : "+ message);
    var nameFile = modelName.replace(/:/g, "_");
    nameFile=nameFile.replace(/\//g, "_");
    
    // Save message and message summary to file
    fs.writeFileSync(nameFile+'.txt', `Summary:\n\n\n ${messageSummary}\n\n\n Message: \n\n\n ${message}`);

    cancel("User cancelled the commit");
    // Prompt user to accept, edit, or regenerate the commit message
    choice = await host.select("Choose", [
        {
            value: "commit",
            description: "accept message and commit",
        },
        {
            value: "edit",
            description: "edit message and commit",
        },
        {
            value: "regenerate",
            description: "regenerate message",
        },
        {
            "value": "cancel",
            "description": "exit without committing"
        }
    ])
    if(choice === "cancel") {
        cancel("User cancelled the commit");
    }
    // Handle user's choice for commit message
    if (choice === "edit") {
        message = await host.input("Edit commit message", {
            required: true,
        })
        choice = "commit"
    }
    // If user chooses to commit, execute the git commit and optionally push changes
    if (choice === "commit" && message) {
        console.log("Committing changes with the following message:");
        console.log(message);
        const messageSummary1=`generated on ${date} : ${time} `;
        console.log(await git.exec(["commit","-m",messageSummary, "-m", message]))
        // if (await host.confirm("Push changes?", { default: true }))
        //     console.log(await git.exec("push"))
        // break
    }
} while (choice !== "commit")