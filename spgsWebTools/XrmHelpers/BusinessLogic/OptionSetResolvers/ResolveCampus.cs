using spgsWebTools.XrmHelpers.Shared.Utilities;

namespace spgsWebTools.XrmHelpers.BusinessLogic.OptionSetResolvers
{
    public static int ResolveCampus(string code)
    {
        string norm = SafeString(code);
        int key;

        switch (norm)
        {
            case "pk":
                key = 384810000;
                break;
            case "j":
                key = 384810001;
                break;
            case "s":
                key = 384810002;
                break;
            case "b":
                key = 384810003;
                break;
            default:
                key = 384810004;
                break;
        }

        return key;
    }

    public static int ResolveCampus(int key)
    {
        string code;

        switch (key)
        {
            case 384810000:
                code = "PK";
                break;
            case 384810001:
                code = "J";
                break;
            case 384810002:
                code = "S";
                break;
            case 384810003:
                code = "B";
                break;
            default:
                code = "NOTSEL";
                break;
        }
        return key;
    }
}