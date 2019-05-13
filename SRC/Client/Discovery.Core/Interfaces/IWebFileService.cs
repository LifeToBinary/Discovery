using System.Collections.Generic;

namespace Discovery.Core.Interfaces
{
    /// <summary>
    /// 可以与 Web 文件服务进行交互
    /// </summary>
    public interface IWebFileService
    {
        /// <summary>
        /// 从远程服务器下载文件
        /// </summary>
        /// <param name="localFilePath">本地路径</param>
        /// <param name="originFilePath">远程服务器路径</param>
        /// <returns>True: 上传成功 False: 上传失败, 上传过程中发生了错误</returns>
        bool Upload(string localFilePath, string originFilePath);

        /// <summary>
        /// 从远程服务器下载文件
        /// </summary>
        /// <param name="originFilePath">远程服务器路径</param>
        /// <param name="localFilePath">本地保存位置</param>
        /// <returns>True: 下载成功 False: 下载失败, 下载过程中发生了错误</returns>
        bool Download(string originFilePath, string localFilePath);

        /// <summary>
        /// 删除远程服务器的指定文件
        /// </summary>
        /// <param name="originFilePath">文件路径</param>
        /// <returns>True: 删除成功, False: 删除失败, 在删除过程中发生了错误</returns>
        bool Delete(string originFilePath);
        
        /// <summary>
        /// 在远程服务器创建一个目录
        /// </summary>
        /// <param name="originDirectoryPath">目录路径(包括目录名)</param>
        /// <returns>True: 创建目录成功, False: 创建目录失败, 在创建目录的过程中发生了错误</returns>
        bool MakeDirectory(string originDirectoryPath);
        
        /// <summary>
        /// 更新远程服务器上的文件
        /// </summary>
        /// <param name="localFilePath">本地文件</param>
        /// <param name="originFilePath">远程服务器的目标文件路径</param>
        /// <returns>True: 更新成功 False: 更新失败, 在更新文件的过程中发生了错误</returns>
        bool UpdateFile(string localFilePath, string originFilePath);
        
        /// <summary>
        /// 检查在远程服务器上是否存在指定文件
        /// </summary>
        /// <param name="originFilePath">文件路径</param>
        /// <returns>True: 已存在, False: 不存在</returns>
        bool FileIsExists(string originFilePath);

        /// <summary>
        /// 获取远程服务器指定目录下的文件名列表
        /// </summary>
        /// <param name="originDirectoryPath">远程服务器目录路径</param>
        /// <returns>文件名列表</returns>
        List<string> GetAllFilesList(string originDirectoryPath);
    }
}
