module Sanchez.Logger.Filters.LogLevel

open Sanchez.Logger.Abstraction

let private levelToNumber (level: LogLevel) =
    match level with
    | Debug -> 0
    | Info -> 1
    | Success -> 2
    | Warning -> 3
    | Error -> 4
    | Critical -> 5

let private handleLogMessage (filterLevel: LogLevel) (currentLevel: LogLevel) =
    (levelToNumber filterLevel) > (levelToNumber currentLevel)

let Filter (filterLevel: LogLevel) =
    handleLogMessage filterLevel