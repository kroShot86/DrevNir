# 1. Удаляем папку Temp принудительно
Remove-Item -Path "Temp" -Recurse -Force -ErrorAction SilentlyContinue

# 2. Удаляем все из Git индекса
git rm -r --cached .

# 3. Создаем ИДЕАЛЬНЫЙ .gitignore для Unity
$gitignoreContent = @"
[Ll]ibrary/
[Tt]emp/
[Oo]bj/
[Bb]uild/
[Bb]uilds/
[Ll]ogs/
[Uu]ser[Ss]ettings/
*.csproj
*.sln
*.suo
*.user
*.userprefs
*.pidb
*.vs
.vsconfig
*.log
*.tmp
*.pidb.meta
"@

$gitignoreContent | Out-File .gitignore -Encoding UTF8

# 4. Создаем ПРАВИЛЬНЫЙ .gitattributes
$gitattributesContent = @"
# ВСЕ Unity файлы - как бинарные (никаких CRLF!)
*.anim -text
*.controller -text
*.unity -text
*.asset -text
*.prefab -text
*.mat -text
*.physicsMaterial2D -text
*.physicsMaterial -text
*.preset -text
*.preferences -text
*.mixer -text
*.mask -text
*.flare -text
*.guiskin -text
*.fontsettings -text
*.shadervariants -text
*.inputactions -text
*.meta -text

# ВСЕ ProjectSettings - как бинарные
ProjectSettings/* -text

# Text files
*.cs text eol=crlf
*.shader text eol=crlf
*.txt text eol=crlf
*.json text eol=crlf
*.xml text eol=crlf
*.md text eol=crlf
"@

$gitattributesContent | Out-File .gitattributes -Encoding UTF8

# 5. Настраиваем Git
git config core.autocrlf false
git config core.safecrlf false

# 6. Добавляем только нужные файлы
git add Assets/
git add ProjectSettings/
git add Packages/
git add .gitignore
git add .gitattributes

# 7. Смотрим статус
git status

# 8. Коммит
git commit -m "FIXED: Proper Unity git setup - no more Temp/CRLF issues"
