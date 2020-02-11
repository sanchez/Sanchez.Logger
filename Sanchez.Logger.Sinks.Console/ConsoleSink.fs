module Sanchez.Logger.Sinks.Console

open System
open Sanchez.Logger.Abstraction

let private printConsoleColorWithMessage (color: ConsoleColor) (message: string) =
    Console.ForegroundColor <- color
    Console.Write message

let private renderColor (color: LogColouring) =
    match color with
    | Red s -> printConsoleColorWithMessage ConsoleColor.Red s
    | Green s -> printConsoleColorWithMessage ConsoleColor.Green s
    | Yellow s -> printConsoleColorWithMessage ConsoleColor.Yellow s
    | Blue s -> printConsoleColorWithMessage ConsoleColor.Blue s
    | Magenta s -> printConsoleColorWithMessage ConsoleColor.Magenta s
    | Cyan s -> printConsoleColorWithMessage ConsoleColor.Cyan s
    
    | Faded s -> printConsoleColorWithMessage ConsoleColor.Gray s
    | Reset -> Console.ResetColor ()
    
let private render (symbol: LogSymbols) =
    match symbol with
    | Coloring c ->
        renderColor c
        renderColor Reset
    | Message msg -> Console.Write msg
    
let private convertKey (key: string) =
    "$" + key.ToLower()
    
let rec findCharacterEnding (inputString: char list) (currentBlock: char list) =
    if List.isEmpty inputString then None
    else
        let char = List.head inputString
        if Char.IsLetter char then
            let remainderInputSet = List.tail inputString
            let newCurrentBlock = char::currentBlock
            findCharacterEnding remainderInputSet newCurrentBlock
        else
            Some (inputString, currentBlock |> List.rev |> List.toArray |> System.String)
            
let providerLookForward (inputString: char list) (providers: ProviderMap) =
    findCharacterEnding inputString []
    |> Option.bind (fun (remaindingChars, providerKey) ->
        match Map.tryFind providerKey providers with
        | Some symbols -> Some (remaindingChars, symbols)
        | None -> None)
    
let rec printMessage (providers: ProviderMap) (printFormat: char list) =
    if (List.isEmpty printFormat) then ()
    else
        // Do something with the characters here
        let char = List.head printFormat
        if char = '$' then
            // Do something special here with the character
            match providerLookForward (List.tail printFormat) providers with
            | Some (remainderChars, symbols) ->
                render symbols
                remainderChars |> printMessage providers
            | None ->
                Console.Write '$'
                List.tail printFormat |> printMessage providers
        else
            printFormat |> List.head |> Console.Write
            List.tail printFormat |> printMessage providers
        
let Sink (printFormat: string) (providers: ProviderMap) (message: string) =
    // Print out the formatting
    Seq.toList printFormat |> printMessage providers
    // Print out the actual message
    printfn "%s" message
    
    ()