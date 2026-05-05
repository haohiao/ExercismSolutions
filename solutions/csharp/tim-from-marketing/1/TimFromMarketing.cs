using System.Text;

static class Badge
{
    public static string Print(int? id, string name, string? department)
    {
        StringBuilder builder = new StringBuilder();
        
        builder.Append(id is not null ? $"[{id}] - " : "");
        builder.Append(name);
        builder.Append(" - ");
        builder.Append(department?.ToUpper() ?? "OWNER");
        return builder.ToString();
    }
}
