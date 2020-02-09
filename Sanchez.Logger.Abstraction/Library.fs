namespace Sanchez.Logger.Abstraction
    
type LogLevel =
    | Critical
    | Error
    | Warning
    | Success
    | Info
    | Debug
    
type ProviderPayload = { key: string option; value: string }
type ProviderCall = LogLevel -> ProviderPayload
type ProviderInfo = { name: string; handler: ProviderCall }
    
type LoggerCall = LogLevel -> string -> unit
type Logger<'T> = LogLevel -> Printf.StringFormat<'T> -> unit

type ILoggerFactory =
    {
        sinks: LoggerCall list
        providers: ProviderInfo list
    }