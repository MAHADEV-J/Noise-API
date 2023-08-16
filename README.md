# Noise-API
A web API that returns noise.

This web API uses the FastNoiseLite class by Auburn (https://github.com/Auburn/FastNoiseLite) to generate noise and return that in an HTTP web response.
It has to run on a Windows server because it uses the System.Drawing.Common.Bitmap class which is only available on Windows.

TODO:
* make a separate branch where I try this shit without async