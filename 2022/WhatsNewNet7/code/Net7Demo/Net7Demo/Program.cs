using System.ComponentModel.DataAnnotations;
using System.Formats.Tar;

WriteLine("Net 7!");
WriteLine("make tar");
var dir = GetCurrentDirectory();
if(dir.EndsWith(Path.DirectorySeparatorChar))
    dir=dir.Substring(0, dir.Length - 1);

string nameTar =dir + "andrei.tar" ;
if (File.Exists(nameTar)) File.Delete(nameTar);
WriteLine($"archive {dir} to {nameTar}");
TarFile.CreateFromDirectory(dir,nameTar,true);


