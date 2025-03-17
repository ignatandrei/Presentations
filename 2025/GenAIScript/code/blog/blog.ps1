cls
$text = "";
$modelNames =  @('ollama:llama3.2','ollama:deepseek-r1',"ollama:phi4","ollama:gemma2:27b","ollama:smallthinker","ollama:falcon3:10b","ollama:dolphin3")
foreach ( $modelName in $modelNames )
{
    $sw = [Diagnostics.Stopwatch]::StartNew()
    $a = npx genaiscript run blogpost blog.txt --model $modelName   | Out-String
    $sw.Stop()
    $index= $a.IndexOf($modelName)    
    Write-Host "found " $index "for "   $modelName 
    $text += " `r`n <h1>Model Name "+ $modelName + "</h1> `r`n" +$a.Substring($index+ $modelName.Length)
    $text +="`r`n----------------------`r`n"
    $text += $modelName + " took "+ $sw.Elapsed.TotalMinutes + " minutes"
    $text +="`r`n----------------------`r`n"
    remove-item blogAI.txt
    $text | Out-File blogAI.txt    
        #    Write-Host $a.Substring($index+ $modelName.Length)
}
remove-item blogAI.txt
$text | Out-File blogAI.txt
return
exit

# npx genaiscript run blogpost blog.txt     --model "ollama:deepseek-r1"
