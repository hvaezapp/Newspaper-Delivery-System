namespace NDS.Models.Services
{
    public interface IDeleteFile
    {


        void DeleteFileFromHost(string filename, string path1, string path2 = "");
    }
}
