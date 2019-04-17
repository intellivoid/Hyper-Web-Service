# Intellivoid Hyper Web Service

Hyper Web Service (HyperWS) is a .NET Library which allows
.NET Applications to create local/remote Web Servers which
are designed to provide a web service, such as a JSON APIs
and or interactive Web Applications.

All the requests are designed to be handled by your software
and all the responses are designed to also be handled by your
software.


HyperWS works with Mono and standard .NET Framework.

------------------------------------------------------------------

## Example Usage

To construct the Web Server, you can follow the example usage
presented below. This will use any random port that's
available on your localhost. This also only allows localhost
requests.

```csharp
var server = new Intellivoid.HyperWS.HttpServer
server.EndPoint = new IPEndPoint(IPAddress.Loopback, 0);
```

You can alter the port that's used, and still allow localhost
only requests.

```csharp
var server = new Intellivoid.HyperWS.HttpServer
server.EndPoint = new IPEndPoint(IPAddress.Loopback, 8080);
```

If you want to allow connections from within your network or even
outside your network, you can listen for any incoming requests
from any host rather than just your localhost by using 
`IPAddress.Any` instead of `IPAddress.Loopback`

```csharp
var server = new Intellivoid.HyperWS.HttpServer
server.EndPoint = new IPEndPoint(IPAddress.Any, 0);
```

If the port number is set to `0`, a random port will be assigned.
The port number cannot be greater than `65535`, if the port
is already used, an exception will be thrown.

Using `Start()` and `Stop()` will allow you to control the current
state of the server, note that `Start()` is a non-blocking 
method.

```csharp
// Construct the server
var server = new Intellivoid.HyperWS.HttpServer
server.EndPoint = new IPEndPoint(IPAddress.Any, 0);

// Start the server
server.Start();

// Print the IP:Port that's being used right now.
Console.WriteLine(server.EndPoint);

// Press return to kill the server.
Console.ReadLine();
server.Stop();

```


### Handling Incoming Requests.

An event `RequestReceived` is raised from the object `HttpServer`
whenever an incoming request is received. Using 
`HttpRequestEventArgs` you can determine who is sending the
request, what URL Query is being used, what request method
is being used, etc.

a simple function which handles these events can be something
like this

```csharp
public void RequestReceived(object sender, Intellivoid.HyperWS.HttpRequestEventArgs httpRequest)
{
    // ...
}
```

a full usage of this would be something like this

```csharp
public void StartServer()
{
    // Construct the server
    var server = new Intellivoid.HyperWS.HttpServer
    server.EndPoint = new IPEndPoint(IPAddress.Any, 0);

    //Set the Event Listener
    server.RequestReceived += RequestReceived;

    // Start the server
    server.Start();

    // Print the IP:Port that's being used right now.
    Console.WriteLine(server.EndPoint);

    // Press return to kill the server.
    Console.ReadLine();
    server.Stop();
}


public void RequestReceived(object sender, Intellivoid.HyperWS.HttpRequestEventArgs httpRequest)
{
    var requestPath = httpRequest.Request.Path.Split('/');

    switch(requestPath[1])
    {
        case "": // Request: "http://127.0.0.1:80/"
            break;
        
        case "foo": // Request: "http://127.0.0.1:80/foo"
            break;
    }
}
```


### Sending Responses Back

Using the example above, when using 
`Intellivoid.HyperWS.HttpRequestEventArgs` you can send responses
back to the client using `Response`.

Sending plain text back to the client is simplified by using
the Output Stream

```csharp
private static void SendResponse(Intellivoid.HyperWS.HttpRequestEventArgs httpRequest, string content, int statusCode)
{
    httpRequest.Response.Headers.Add("X-Powered-By", "HyperWS");
    httpRequest.Response.StatusCode = statusCode;
    using (var writer = new StreamWriter(httpRequest.Response.OutputStream))
    {
        writer.Write(content);
    }
}
```

And to send stuff like images, files, etc. It's almost the same

```csharp
private static void SendFile(Intellivoid.HyperWS.HttpRequestEventArgs httpRequest, string filepath)
{
    httpRequest.Response.Headers.Add("X-Powered-By", "HyperWS");
    httpRequest.Response.StatusCode = 200;
    using (var stream = File.OpenRead(filepath))
    {
        stream.CopyTo(httpRequest.Response.OutputStream);
    }
}
```

# License
Attribution-ShareAlike 4.0 International (CC BY-SA 4.0)

## You are free to
 - copy and redistribute the material in any medium or format
 - remix, transform, and build upon the material for any purpose, even commercially.


## Under the following terms

 - You must give appropriate credit and indicate if changes were made. You may do so in any reasonable manner, but not in any way that suggests the licensor endorses you or your use.
 
 - If you remix, transform, or build upon the material, you must provide credit

## Notice

You do not have to comply with the license for elements of the material in the public domain or where your use is permitted by an applicable exception or limitation.

No warranties are given. The license may not give you all of the permissions necessary for your intended use. For example, other rights such as publicity, privacy, or moral rights may limit how you use the material.