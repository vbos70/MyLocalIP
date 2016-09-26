module Program
open System.IO
open System.Net
open System.Net.Sockets

let fname = """C:\Users\vbos\Documents\SpiderOak Hive\lt141016-03-ip.txt"""
let savedIP = try
                File.ReadLines(fname) |> Seq.head
              with
                  | ex -> "No saved IP could be read"

let myLocalIP = try
                  ((Dns.GetHostEntry (Dns.GetHostName ())).AddressList
                   |> Array.filter (fun (ip : IPAddress) -> ip.AddressFamily = AddressFamily.InterNetwork)
                   |> Array.head).ToString()
                with
                    | ex -> "Unknown local IP"

[<EntryPoint>]
let main argv =
    if savedIP <> myLocalIP
    then
        try
            let stream = new StreamWriter(fname, false)
            stream.WriteLine(myLocalIP)
            stream.Flush()
            stream.Close()
            0
        with  // catch all exceptions
            | ex -> 1 // Non-zero return status of main indicates that an error has occurred.
    else
        0 // Zero return status of main indicates that no error has occurred.
