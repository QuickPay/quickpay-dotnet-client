# quickpay-dotnet-client
`quickpay-dotnet-client` is a official client for [QuickPay API](http://tech.quickpay.net/api). The Quickpay API enables you to accept payments in a secure and reliable manner.  
This assembly currently support QuickPay `v10` api.

## installation
via nuget, comming up!!!  

## Usage

Before doing anything you should register yourself with QuickPay and get access credentials. 
If you haven't please [click](https://quickpay.net/) here to apply.

### The base class

This client provides a base class for calling the QuickPay API, the base class is called `QuickPayRestClient.  
The base class will set the correct headers and credentials for you to call the service.  
The base class will also validate the response.  
Furthermore a basic implementation of paging and sorting exists.  
  
This base class can be used to call the api.  
  
You can either create a class with the properties you need to get returned, see `merchantexample.cs` for examples of this.   
Or you can simply return a dictionary from the service, for example of this please see `pingexample.cs`.  
You can call the service async, please see `pingexample.cs` for an example of this.  

#### The unit tests

To run the tests you need to set the corresponding environment variables, please see qpconfig.cs for more info.  