module Sanchez.Logger.Sinks.File

open System.IO
open Sanchez.Logger.Abstraction

type FileConfiguration =
    {
        fileLocation: string option
    }

let Sink (fileConfig: FileConfiguration) =
    let fileName = fileConfig.fileLocation |> Option.defaultWith Path.GetTempFileName
    let writer = File.AppendText fileName
    
    if fileConfig.fileLocation |> Option.isNone then
        printfn "Log Output: %s" fileName
    
    let handler (level: LogLevel) (message: string) =
        ()
        
    handler