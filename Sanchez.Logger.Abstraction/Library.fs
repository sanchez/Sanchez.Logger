namespace Sanchez.Logger.Abstraction
    
type LogLevel =
    | Critical
    | Error
    | Warning
    | Success
    | Info
    | Debug
    
type LogColouring =
    | Red of string
    | Green of string
    | Yellow of string
    | Blue of string
    | Magenta of string
    | Cyan of string
    
    | Faded of string
    
    | Reset
    
type LogSymbols =
    | Coloring of LogColouring
    | Message of string
    
type Provider = LogLevel -> LogSymbols option
type ProviderInfo = { name: string; handler: Provider }
type ProviderMap = Map<string, LogSymbols>
    
type LoggerCall = ProviderMap -> string -> unit
type Logger = LogLevel -> string -> unit

type ILoggerFactory =
    {
        sinks: LoggerCall list
        providers: ProviderInfo list
    }