import re
import pyperclip

propRegex = re.compile(r"(\w+) (\w+) { get; set; }")

def ExtractProps(text):
    props = []
    for m in propRegex.finditer(text):
        props.append((m.group(1), m.group(2)))
    return props

def PropToSharp(prop):
    dir = "StyleTableProperties."
    return "destination." + dir + prop[1] + " = (" + prop[0] + ")source." + dir + prop[1] + "?.Clone() ?? destination." + dir + prop[1] + ";"

text = None
with open(r"D:\Programing Stuff\C#\DocxToTxt\DocxToTxt\Scripts\SharpText.txt", "rt") as f:
    text = f.read()
    
props = ExtractProps(text)
sharpText = "\n".join([PropToSharp(p) for p in props])
pyperclip.copy(sharpText)