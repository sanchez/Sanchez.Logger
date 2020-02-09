module Sanchez.Logger.Sinks.File

open System
open System.IO
open Sanchez.Logger.Abstraction

type FileConfiguration =
    {
        fileLocation: string option
    }

let Sink (fileConfig: FileConfiguration) =
    let fileName = fileConfig.fileLocation |> Option.defaultWith Path.GetTempFileName
    use writer = File.AppendText fileName
    
    if fileConfig.fileLocation |> Option.isNone then
        printfn "Log File Output Location: %s" fileName
    
    let handler (providers: ProviderMap) (message: string) =
        ()
        
    handler