using Salolab1oop;
using System;

Console.WriteLine("Hewwo, World!");
Saloclass1 miau;
miau = new Saloclass1(25, -12, 300);
Saloclass1 woof;
woof = new Saloclass1(1, 25, 4);

Console.WriteLine("Hue " + miau.Gethue());
Console.WriteLine("Saturation " + miau.Getsaturation());
Console.WriteLine("lightness " + miau.Getlightness());

Console.WriteLine("Miau+woof:");
Console.WriteLine("Hue: " + (miau + woof).Gethue());
Console.WriteLine("Hue: " + (miau + woof).Getsaturation());
Console.WriteLine("Hue: " + (miau + woof).Getlightness());

Console.WriteLine("ToString result: " + miau.ToString());