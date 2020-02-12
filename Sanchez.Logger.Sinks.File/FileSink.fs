module Sanchez.Logger.Sinks.File

open FSharp.Json
open System
open System.IO
open Sanchez.Logger.Abstraction

type FileConfiguration =
    {
        fileLocation: string option
    }
    
let private renderColor (color: LogColouring) =
    match color with
    | Red s | Green s | Yellow s | Blue s | Magenta s | Cyan s | Faded s -> s
    | Reset -> ""
    
let private render (symbol: LogSymbols) =
    match symbol with
    | Coloring c -> renderColor c
    | Message msg -> msg
    
let private handleLogMessage (writer: StreamWriter) (providers: ProviderMap) (message: string) =
    let line = 
        providers
        |> Map.map (fun _ -> render)
        |> Map.add "message" message
        |> Json.serialize
        
    writer.WriteLine line
    writer.Flush ()

let Sink (fileConfig: FileConfiguration) =
    let fileName = fileConfig.fileLocation |> Option.defaultWith Path.GetTempFileName
    let writer = File.AppendText fileName
    
    if fileConfig.fileLocation |> Option.isNone then
        printfn "Log File Output Location: %s" fileName
    
    handleLogMessage writer