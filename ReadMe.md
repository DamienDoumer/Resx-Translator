# ResX Translator

This is a **.resx** Resource file automatic translation tool. It is an extremely simple tool, and uses Google translation API to perform its translations. 

## How to Use It
Follow [these instructions](https://cloud.google.com/translate/docs/reference/libraries?hl=fr) to subscribe to Google Cloud's translation API, 
Add the **json** file containing your API key's info in the same directory as the executable file.
Then run it as a simple CLI tool. With the following flags: 

**-t,  --translations** is the flag to list the translation languages which you want your resource file to be translated to. 

**-f, --file** The path to your resource file

**-l, --language** The language of your current resource file.

**-p, --path** The path where your Resx will be created. The default path is in your documents folder.

**-k, --key** Google translation API Key

**-e, --existing** If flag is present, then it will look for an existing translation and only update new occurances.

### Example 
-t hi,pt,fr,ru -f  "C:\Users\john\Desktop\Resources.resx"  -p  "C:\Users\john\Desktop"

## Build for Global Use

Running `./build_release.sh` locally which produces a directory with a `lib` folder (the built project) and a `bin` folder (a convenience script which calls `mono` to run the program. This can be installed globally to be able to run this program anywhere.

```(sh)
./build_release.sh
cp Release/bin/resx_translator /usr/local/bin/
cp -R Release/lib/ /usr/local/lib/
```

### Example
`resx_translator -t hi,pt,fr,ru -f "../Strings/Resources.resx"  -p  "../Strings" -k secret_key.json`
