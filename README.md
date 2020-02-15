# Sanchez.Logger

This library is aimed to providing a functional logger for dotnet languages, in particular f#.

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

## Children
It is possible to combine multiple factories together through a child/parent relationship. A reason to do this might be that you want the console to print all log messages while only the file output receives the warning and error messages. To achieve this behaviour you create two separate factories and then add one as a child to the other. Now when the parent factory is called it will also chain the call down to the child.

# Setting  Up
In order to setup your logging environment you can either:
 
 - download the packages required to setup your logging environment; or
 - download and use a template
 
 ## Templates
 Templates are provided for quick and easy setup of a logging environment. The provided template (Sanchez.Logger.Templates.Standard) configures console sink and file sink. The console will log all statements while the file will only log warning messages or above.

# Logging

## Creating a factory
A logger is configured through creating factory and adding the extensions required to the logger. `LoggerFactory.CreateFactory()` will create a blank factory for extensions to be added to.

`LoggerFactory.AddProvider` is used to add a provider to the factory and the return is a new factory with the provider registered and configured.
`LoggerFactory.AddSink` is used to add a sink to the factory and the return is a new factory with the sink registered and configured.
`LoggerFactory.AddFilter` is used to add a filter to the factory and the return is a new factory with the filter registered and configured.
`LoggerFactory.AddChild` is used to add a factory as a child to the current factory, the result is the parent factory with the child registered and configured.

## Logging
Once the factory is configured to the desired effect, it can be **built** to allow the user to begin logging. To build a factory simply call the `LoggerFactory.BuildLogger` with the factory. The resulting object is a function which has the following signature: `LoggerLevel -> string -> unit`. Call this function with a `LogLevel` and a message for the logger to execute.

# Example
There is an extensive example in the Sanchez.Loggers.Example package
