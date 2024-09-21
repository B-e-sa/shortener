using System;

namespace Shortener.Domain.Common;

public static class CodeGenerator
{
    public static string Numbers(int length)
    {
        string code = "";
        string chars = "0123456789";

        Random random = new();
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(0, chars.Length);
            code += chars[index];
        }

        return code;
    }

    public static string Alphanumeric(int length)
    {
        string code = "";
        string chars = "abcdefghijklmnopqrstuvwxyzABCDEFGIJKLMNOPQRSTUVWXYZ0123456789";

        Random random = new();
        for (int i = 0; i < length; i++)
        {
            int index = random.Next(0, chars.Length);
            code += chars[index];
        }

        return code;
    }
}