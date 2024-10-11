# ARCHIVED

This project is no longer maintained and will not receive any further updates. If you plan to continue using it, please be aware that future security issues will not be addressed.

<h3 align="center">
    <img src="./media/52n-logo.svg" height=100>
    <p><b>WPS.NET</b></p>
</h3>

<p align="center">
    <a href="#description">Description</a> •
    <a href="#installation">Installation</a> •
    <a href="#how-usage">Usage</a> •
    <a href="#license">License</a>
</p>

# WPS<span>.</span>NET

## Description

This project was created initially during the [2019th GSoC](https://summerofcode.withgoogle.com/projects/#6595064984240128).

Its purpose is mainly to allow users to integrate the WPS standard in their program seamlessly without having to reimplement the specification every time. The library was built in C# to integrate with the .NET ecosystem. It is easily extensible and featureful providing the users the two main implementation of execution types, synchronous and asynchronous.

## Installation

You can get the package from NuGet.

### .NET CLI

```dotnet add package WPS.NET```

### Packet Manager

```Install-Package WPS.NET```

## Usage

### Getting the server capabilities

```cs
var client = new WpsClient(httpClient, xmlSerializer);
var capabilities = await client.GetCapabilities("server.uri");
```

### Describing a process

```cs
var client = new WpsClient(httpClient, xmlSerializer);
var processOfferings = await client.DescribeProcess("server.uri", "process identifier");
```

### Creating a request

```cs
var request = new ExecuteRequest
{
    Identifier = "process identifier",
    Inputs = new[]
    {
        new DataInput
        {
            Identifier = "input identifier",
            Data = new LiteralDataValue{ Value = "test" }
        }
    },
    Outputs = new[]
    {
        new DataOutput
        {
            Identifier = "output identifier"
        }
    },
    ExecutionMode = ExecutionMode.Synchronous,
    ResponseType = ResponseType.Raw
};
```

### Executing a request

```cs
var client = new WpsClient(httpClient, xmlSerializer);
var session = await client.AsyncGetDocumentResultAs<YourResultDataType>("server.uri", request);

var pollingTask = session.StartPolling();
session.Finished += (sender, args) => { /* Use the result here. */ };
session.Failed += (sender, args) => { /* The execution has failed. */ };
session.Polled += (sender, args) => { /* The sesssion poller has checked for the status. */ };
```

## License
[Apache v2.0](https://www.apache.org/licenses/LICENSE-2.0)
