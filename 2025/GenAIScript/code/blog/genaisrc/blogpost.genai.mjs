import fs from 'fs';
console.log("test");
console.log(env.files);    

const args = process.argv.slice(2);
const modelArgIndex = args.indexOf("--model");
const modelName = modelArgIndex !== -1 ? args[modelArgIndex + 1] : "ollama:gemma2:27b";
console.log("Model name: " + modelName);
def("FILE", env.files)
script({
    model: modelName,
    system: ["system"],
})

$`

Given the text in FILE, write a blog post that makes the paper sound exciting and encourages readers to look at it. 

Keep the links.
Do NOT use markdown , but just html tags.

`