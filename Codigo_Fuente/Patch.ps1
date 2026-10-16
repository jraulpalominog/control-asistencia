$file = "FormAdmin.Designer.cs"
$content = Get-Content -Path $file -Raw

# We need to restore the lost controls first.
# Wait, let me just restore the whole FormAdmin.Designer.cs using git.
