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
    //printfn "saved IP: %s" savedIP
    //printfn "Local IP: %A" myLocalIP
    if savedIP <> myLocalIP
    then
        try
            let stream = new StreamWriter(fname, false)
            stream.WriteLine(myLocalIP)
            stream.Flush()
            stream.Close()
        with
            | ex -> ()
    0 // Return an integer exit code
