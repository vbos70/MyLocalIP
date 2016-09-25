module Program
open System.Net
open System.Net.Sockets

let myLocalIP = (Dns.GetHostEntry (Dns.GetHostName ())).AddressList
                |> Array.filter (fun (ip : IPAddress) -> ip.AddressFamily = AddressFamily.InterNetwork)
                |> Array.head

[<EntryPoint>]
let main argv =
    printfn "Local IP: %A" myLocalIP
    0 // Return an integer exit code
