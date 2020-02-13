# Sanchez.Logger

Check out the `Sanchez.Logger.Example` for the time being, I will add more here with time

# Definitions
## Sinks
Sinks are a way of reporting a log event to a source (e.g. Console). The currently provided sinks are:

- Sanchez.Logger.Sinks.Console (Used for printing log messages out to the console)
- Sanchez.Logger.Sinks.File (Used for writing messages to a file)

## Providers
Providers are used for adding information to a log message which the sinks can then report on and use in their sink. The currently provided providers are:

- Sanchez.Logger.Providers.LogLevel
- Sanchez.Logger.Providers.Scope (Allows the user to define a custom scope for different log messages)
- Sanchez.Logger.Providers.Timestamp (Adds the timestamp of the moment when the log message occurred)

## Filters
Filters allow for log messages to be conditionally ignored based on condition supplied by the filter. THe currently provided filters are:

- Sanchez.Logger.Filters.LogLevel

# Setting  Up
In order to setup your logging environment

# Logging

# Example