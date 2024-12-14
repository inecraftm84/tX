using System;
using System.IO;
using System.Text;
using System.Threading;

class tX
{
    static void Main()
    {
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        Random rand = new Random();

        while (true)
        {
            // 生成隨機亂碼
            string randomString = GenerateRandomString(30); // 設定亂碼長度為30個字符

            // 使用 GUID 生成隨機亂碼作為檔案名稱
            string randomFileName = randomString + "_" + Guid.NewGuid().ToString() + ".txt";

            // 過濾掉不合法的字符
            randomFileName = SanitizeFileName(randomFileName);

            // 設定隨機檔案的路徑
            string randomFilePath = Path.Combine(desktopPath, randomFileName);

            // 確保文件不會覆蓋已經存在的文件
            while (File.Exists(randomFilePath))
            {
                randomString = GenerateRandomString(30); // 重新生成隨機亂碼
                randomFileName = randomString + "_" + Guid.NewGuid().ToString() + ".txt";
                randomFileName = SanitizeFileName(randomFileName); // 過濾掉不合法的字符
                randomFilePath = Path.Combine(desktopPath, randomFileName);
            }

            // 生成隨機數字和 GUID 並將它們寫入文件
            string content = GenerateRandomString(30) + "_" + Guid.NewGuid().ToString();
            File.WriteAllText(randomFilePath, content);

            // 設定延遲時間，避免過度消耗資源
            Thread.Sleep(1000); // 可以調整延遲時間
        }
    }

    // 生成隨機亂碼
    static string GenerateRandomString(int length)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*()_+-=[]{}|;:',.<>?/~`";
        StringBuilder stringBuilder = new StringBuilder();
        Random rand = new Random();

        for (int i = 0; i < length; i++)
        {
            stringBuilder.Append(chars[rand.Next(chars.Length)]);
        }

        return stringBuilder.ToString();
    }

    // 過濾掉不合法的檔案名稱字符
    static string SanitizeFileName(string fileName)
    {
        // 定義非法字符
        char[] invalidChars = Path.GetInvalidFileNameChars();

        foreach (var invalidChar in invalidChars)
        {
            fileName = fileName.Replace(invalidChar.ToString(), "");
        }

        return fileName;
    }
}
