using System;
using static System.Net.Mime.MediaTypeNames;

namespace _8086sim
{
    public  class Program
    {
        static string AXContent = "0x0000", AHContent = "0x00", ALContent = "0x00";
        static string BXContent = "0x0000", BHContent = "0x00", BLContent = "0x00";
        static string CXContent = "0x0000", CHContent = "0x00", CLContent = "0x00";
        static string DXContent = "0x0000", DHContent = "0x00", DLContent = "0x00";

        static void  Main(string[] args)
        {

            string[] data = {""};
            while(true)
            {
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine($" AX = {AXContent} BX = {BXContent} CX = {CXContent} DX = {DXContent}  ");
                Console.WriteLine();
                Console.WriteLine($" AH = {AHContent} AL = {ALContent} BH = {BHContent} BL = {BLContent} CH = {CHContent} CL = {CLContent} DH = {DHContent} DL = {DLContent}");
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine("HELP <command> -> shows examples for using certain command.");
                Console.WriteLine("MOV <destination> <source> -> MOV copies data from source to target destination.");
                Console.WriteLine();
                Console.WriteLine();

                data = Console.ReadLine().Split();
                Console.Clear();
                switch (data[0])
                {
                    case "HELP":
                        if (data.Length == 2)
                        {
                            Help(data[1]);
                        }
                        else
                        {
                            Console.WriteLine($"Invalid command use.");
                        }
                        break;
                    case "MOV":
                        if (data[1] == "AX" || data[1] == "BX" 
                         || data[1] == "CX" || data[1] == "DX" 
                         || data[1] == "AL" || data[1] == "AH" 
                         || data[1] == "BL" || data[1] == "BH" 
                         || data[1] == "CL" || data[1] == "CH" 
                         || data[1] == "DL" || data[1] == "DH")
                        {
                            if (data.Length == 3)
                            {
                                Console.WriteLine($"MOV-ing {data[2]} -> {data[1]} ...");
                                if (data[2] == "AX" || data[2] == "BX" 
                                 || data[2] == "CX" || data[2] == "DX")
                                {
                                    MOV(data[1], GetMainReg(data[2]));
                                }
                                else if (data[2] == "AL" || data[2] == "AH" 
                                      || data[2] == "BL" || data[2] == "BH" 
                                      || data[2] == "CL" || data[2] == "CH" 
                                      || data[2] == "DL" || data[2] == "DH")
                                {
                                    MOV(data[1], GetSubReg(data[2]));
                                }
                                else if (data[2].Substring(data[2].Length - 1) == "h")
                                {
                                    MOV(data[1], data[2].TrimEnd('h'));
                                }
                                else
                                {
                                    MOV(data[1], ToHex(data[2]));
                                }
                            }
                            else
                            {
                                Console.WriteLine($"Invalid data amount.");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Invalid command use.");
                        }
                        break;

                    case "EXIT":
                        return;
                    default:
                        Console.WriteLine("Invalid command.");
                        break;
                }
            }
    }
        private static string GetSubReg(string s1)
        {
            switch (s1)
            {
                case "AL":
                    char[] al = ALContent.ToString().ToCharArray();
                    return $"{al[2]}{al[3]}";
                    break;
                case "AH":
                    char[] ah = AHContent.ToString().ToCharArray();
                    return $"{ah[2]}{ah[3]}";
                    break;
                case "BL":
                    char[] bl = BLContent.ToString().ToCharArray();
                    return $"{bl[2]}{bl[3]}";
                    break;
                case "BH":
                    char[] bh = BHContent.ToString().ToCharArray();
                    return $"{bh[2]}{bh[3]}";
                    break;
                case "CL":
                    char[] cl = CLContent.ToString().ToCharArray();
                    return $"{cl[2]}{cl[3]}";
                    break;
                case "CH":
                    char[] ch = CHContent.ToString().ToCharArray();
                    return $"{ch[2]}{ch[3]}";
                    break;
                case "DL":
                    char[] dl = DLContent.ToString().ToCharArray();
                    return $"{dl[2]}{dl[3]}";
                    break;
                case "DH":
                    char[] dh = DHContent.ToString().ToCharArray();
                    return $"{dh[2]}{dh[3]}";
                    break;

                default:
                    return "0000";
                    break;
            }
        }

        private static string GetMainReg(string s1)
        {
            switch (s1)
            {
                case "AX":
                    char[] ax = AXContent.ToString().ToCharArray();
                    return $"{ax[2]}{ax[3]}{ax[4]}{ax[5]}";
                    break;
                case "BX":
                    char[] bx = BXContent.ToString().ToCharArray();
                    return $"{bx[2]}{bx[3]}{bx[4]}{bx[5]}";
                    break;
                case "CX":
                    char[] cx = CXContent.ToString().ToCharArray();
                    return $"{cx[2]}{cx[3]}{cx[4]}{cx[5]}";
                    break;
                case "DX":
                    char[] dx = DXContent.ToString().ToCharArray();
                    return $"{dx[2]}{dx[3]}{dx[4]}{dx[5]}";
                    break;
                default:
                    return "0000";
                    break;
            }
        }

        private static string ToHex(string s1)
        {
            try
            {
                int t = int.Parse(s1);
                return t.ToString("X");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message + " Registry cleared.");
                return "0000";
            }
        }

        private static void MOV(string s1, string s2)
        {
            SetRegisterContent(s1, s2);
        }

        private static void SetRegisterContent(string type, string data)
        {
            switch (type)
            {
                case "AX":
                    data = CheckRegLength4(data);
                    AXContent = $"0x{data}";
                    char[] c = data.ToCharArray();
                    AHContent = $"0x{c[0]}{c[1]}";
                    ALContent = $"0x{c[2]}{c[3]}";
                    break;
                case "BX":
                    data = CheckRegLength4(data);
                    BXContent = $"0x{data}";
                    char[] c1 = data.ToCharArray();
                    BHContent = $"0x{c1[0]}{c1[1]}";
                    BLContent = $"0x{c1[2]}{c1[3]}";
                    break;
                case "CX":
                    data = CheckRegLength4(data);
                    CXContent = $"0x{data}";
                    char[] c2 = data.ToCharArray();
                    CHContent = $"0x{c2[0]}{c2[1]}";
                    CLContent = $"0x{c2[2]}{c2[3]}";
                    break;
                case "DX":
                    data = CheckRegLength4(data);
                    DXContent = $"0x{data}";
                    char[] c3 = data.ToCharArray();
                    DHContent = $"0x{c3[0]}{c3[1]}";
                    DLContent = $"0x{c3[2]}{c3[3]}";
                    break;
                case "AH":
                    SetMainRegister("AX", CheckRegLength2(data), "H");
                    break;
                case "AL":
                    SetMainRegister("AX", CheckRegLength2(data), "L");
                    break;
                case "BH":
                    SetMainRegister("BX", CheckRegLength2(data), "H");
                    break;
                case "BL":
                    SetMainRegister("BX", CheckRegLength2(data), "L");
                    break;
                case "CH":
                    SetMainRegister("CX", CheckRegLength2(data), "H");
                    break;
                case "CL":
                    SetMainRegister("CX", CheckRegLength2(data), "L");
                    break;
                case "DH":
                    SetMainRegister("DX", CheckRegLength2(data), "H");
                    break;
                case "DL":
                    SetMainRegister("DX", CheckRegLength2(data), "L");
                    break;
                default:
                    break;
            }
        }

        private static void SetMainRegister(string type, string data, string pos)
        {
            switch (type)
            {
                case "AX":
                    char[] ax = AXContent.ToString().ToCharArray();
                    if (pos == "L")
                    {
                        ALContent = $"0x{data}";
                        data = $"0x{ax[2]}{ax[3]}{data}";
                        AXContent = data;
                    }
                    else if (pos == "H")
                    {
                        AHContent = $"0x{data}";
                        data = $"0x{data}{ax[4]}{ax[5]}";
                        AXContent = data;
                    }
                    break;
                case "BX":
                    char[] bx = BXContent.ToString().ToCharArray();
                    if (pos == "L")
                    {
                        BLContent = $"0x{data}";
                        data = $"0x{bx[2]}{bx[3]}{data}";
                        BXContent = data;
                    }
                    else if (pos == "H")
                    {
                        BHContent = $"0x{data}";
                        data = $"0x{data}{bx[4]}{bx[5]}";
                        BXContent = data;
                    }
                    break;
                case "CX":
                    char[] cx = CXContent.ToString().ToCharArray();
                    if (pos == "L")
                    {
                        CLContent = $"0x{data}";
                        data = $"0x{cx[2]}{cx[3]}{data}";
                        CXContent = data;
                    }
                    else if (pos == "H")
                    {
                        CHContent = $"0x{data}";
                        data = $"0x{data}{cx[4]}{cx[5]}";
                        CXContent = data;
                    }
                    break;
                case "DX":
                    char[] dx = DXContent.ToString().ToCharArray();
                    if (pos == "L")
                    {
                        DLContent = $"0x{data}";
                        data = $"0x{dx[2]}{dx[3]}{data}";
                        DXContent = data;
                    }
                    else if (pos == "H")
                    {
                        DHContent = $"0x{data}";
                        data = $"0x{data}{dx[4]}{dx[5]}";
                        DXContent = data;
                    }
                    break;
                default:
                    break;
            }
        }

        private static string CheckRegLength4(string data)
        {
            if (data.Length == 4)
            {
                return data;
            }
            else if (data.Length < 4)
            {
                for (int i = data.Length; i < 4; i++)
                {
                    data = "0" + data;
                }
                return data;
            }
            else if (data.Length > 4)
            {
                Console.WriteLine("Exceeded registry limit");
                return "0000";
            }
            else
            {
                return "0000";
            }
        }

        private static string CheckRegLength2(string data)
        {
            if (data.Length == 2)
            {
                return data;
            }
            else if (data.Length < 2)
            {
                for (int i = data.Length; i < 2; i++)
                {
                    data = "0" + data;
                }
                return data;
            }
            else if (data.Length > 2)
            {
                Console.WriteLine("Exceeded registry limit");
                return "00";
            }
            else
            {
                return "00";
            }
        }

        private static void Help(string s1)
        {
            switch (s1)
            {
                case "MOV":
                    Console.WriteLine("MOV AX BX -- Copies value of registry BX to AX.");
                    Console.WriteLine("MOV AX 55 -- Inserts value 55(decimal) to AX.");
                    Console.WriteLine("MOV AX 0Bh -- Inserts value 0x000B(hexadecimal) to AX.");
                    Console.WriteLine("MOV AL 0Bh -- Inserts value 0x0B(hexadecimal) to AL.");
                    break;
                default:
                    Console.WriteLine($"{s1} is not implemented");
                    break;
            }
        }
    }
}