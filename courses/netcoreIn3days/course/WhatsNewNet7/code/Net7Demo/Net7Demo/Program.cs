
WriteLine("Net 7!");
WriteLine("make tar");
var dir = GetCurrentDirectory();
if(dir.EndsWith(Path.DirectorySeparatorChar))
    dir=dir.Substring(0, dir.Length - 1);

string nameTar =dir + "andrei.tar" ;
if (File.Exists(nameTar)) File.Delete(nameTar);
string nameZip = dir + "andrei.zip";
if (File.Exists(nameZip)) File.Delete(nameZip);
WriteLine($"archive {dir} to {nameTar}");
TarFile.CreateFromDirectory(dir,nameTar,true);
ZipFile.CreateFromDirectory(dir, nameZip,CompressionLevel.SmallestSize, true);

Int32 i = 0;//press F12
//StringSyntaxAttribute.DateTimeFormat
//delete the "" after this line
var d = DateTime.Now.ToString("");
//RSCG
WriteLine(MessageBoxW(IntPtr.Zero, "asd", "asd", 1));
//RSCG
WriteLine(MessageBoxW_LI(IntPtr.Zero, "asd", "asd", 1));
