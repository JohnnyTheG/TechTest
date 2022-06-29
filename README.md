# UoY Tech Test
Implementation of string parsing of operations on Utc Now.

## Usage
To use the library, include the **TechTest** namespace.

![image](https://user-images.githubusercontent.com/20816598/176393085-18ce6f44-3e4b-43fd-9040-8602eb08c5b9.png)

To parse and execute an operation, use **DateTimeOperation.Execute**.

![image](https://user-images.githubusercontent.com/20816598/176392608-fb5c469a-9dc0-43ac-8049-ac88bf1ea63a.png)

If the operation is invalid then an **InvalidOperationException** will be thrown.
![image](https://user-images.githubusercontent.com/20816598/176393413-0304adaf-4bed-4cc2-b1e0-a3ac1bd046e4.png)

## Notes
For this implementation, it was stated not to use external libraries (e.g. [Luxon](https://moment.github.io/luxon/#/)). .NET has inbuilt [DateTime](https://docs.microsoft.com/en-us/dotnet/api/system.datetime?view=net-6.0) functionality but I have purposefully not used this as I was unsure if this counted as an external library and was against the spirit of this exercise. If I were writing this code for real, I would have used this as my method of handling dates and times.

## Support
Any issues, please let me know by emailing **johnny@johnnygray.co.uk**.
