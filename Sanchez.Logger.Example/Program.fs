// Learn more about F# at http://fsharp.org

open Sanchez.Logger
open Sanchez.Logger
open Sanchez.Logger.Abstraction
open System

[<EntryPoint>]
let main argv =
    
    let factory =
        LoggerFactory.CreateFactory()
        |> Sinks.Console.AddSink
        |> LoggerFactory.BuildLogger
        
    factory Debug "Hello World"
    
    printfn "Hello World from F#!"
    0 // return an integer exit code
