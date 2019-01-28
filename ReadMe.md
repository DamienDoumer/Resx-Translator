# ResX Translator

This is a **.resx** Resource file automatic translation tool. It is an extremely simple tool, and uses Google translation API to perform its translations. 

## How to Use It
Follow [these instructions](https://cloud.google.com/translate/docs/reference/libraries?hl=fr) to subscribe to Google Cloud's translation API, 
Add the **json** file containing your API key's info in the same directory as the executable file.
Then run it as a simple CLI tool. With the following flags: 

**-t,  --translations** is the flag to list the translation languages which you want your resource file to be translated to. 

**-t, --file** The path to your resource file

**-l, --language** The language of your current resource file.

**-p, --path** The path where your Resx will be created. The default path is in your documents folder.

### Example 
-t hi,pt,fr,ru -f  "C:\Users\john\Desktop\Resources.resx"  -p  "C:\Users\john\Desktop"

