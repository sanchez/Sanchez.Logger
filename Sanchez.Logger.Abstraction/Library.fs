namespace Sanchez.Logger.Abstraction
    
type LogLevel =
    | Critical
    | Error
    | Warning
    | Success
    | Info
    | Debug
    
type LoggerCall = LogLevel -> string -> unit
type Logger<'T> = LogLevel -> Printf.StringFormat<'T> -> unit

type ILoggerFactory =
    {
        sinks: LoggerCall list
    }