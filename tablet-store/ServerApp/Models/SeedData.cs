using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using ServerApp.Models;

namespace ServerApp
{

    public class SeedData
    {

        public static void SeedDatabase(DataContext context)
        {

            context.Database.Migrate();

            if (context.Products.Count() == 0)
            {
                var s1 = new Supplier
                {
                    Name = "Star Tech",
                    City = "San Jose",
                    State = "CA"
                };
                var s2 = new Supplier
                {
                    Name = "Soccer Town",
                    City = "Chicago",
                    State = "IL"
                };
                var s3 = new Supplier
                {
                    Name = "Chess Co",
                    City = "New York",
                    State = "NY"
                };

                context.Products.AddRange(
                     new Product
                     {
                         Name = "Apple 10.2 Inch 7th Generation iPad MW742 Wi-Fi, 32GB",
                         Description = "In this Apple iPad MW742LL/A, the 10.2 inch LED - backlit multi - touch display has a 2160 x 1620 resolution for 264 pixels per inch and a total of over 3 million pixels.The display also features an oleophobic coating, which helps to reduce fingerprints and smudges and making it an ideal companion for watching movies, creating content, and much more.Powered by the A10 Fusion chip, it will enable users to edit 4K video, play graphics - intensive games, and experience AR apps.Moreover, its Smart Connector offers Smart Keyboard support for comfortable typing, while its display is compatible with the 1st gen Apple Pencil, so users can write and draw.With front and back cameras, the iPad can take photos, capture video, scan documents, make FaceTime calls, and provide AR capabilities.",
                         Brand = "Apple",
                         Price = 469,
                         ProductImgUrl = "iPad_MW742.jpg",
                         ProductFeatures = "Model: Apple iPad MW742 Space Grey,Capacity: 32GB,Camera: 8MP back 1.2MP front,Touch ID fingerprint sensor,Up to 10 hours of battery life",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 4 } }
                     },
                     new Product
                     {
                         Name = "Apple iPad Mini 5 MUQW2 7.9 inch Wi-Fi 64GB",
                         Description = "iPad mini is beloved for its size and capability. And now there are even more reasons to love it. The A12 Bionic chip with Neural Engine. A 7.9 inch Retina display with True Tone. And Apple Pencil, so you can capture your biggest ideas wherever they come to you. It’s still iPad mini. There’s just more of it than ever. iPad mini features a thin, light, and portable design that makes it the perfect on-the-go companion. At 0.66 pound and 6.1 mm thin,1 it’s easy to carry with you in one hand or take out of a pocket or bag whenever inspiration strikes. The A12 Bionic chip with Neural Engine enables a remarkable level of power and intelligence. ",
                         Brand = "Apple",
                         Price = 588,
                         ProductImgUrl = "iPad_Mini_5_MUQW2.jpg",
                         ProductFeatures = "Model: Apple iPad Mini 5 7.9 inch MUQW2LL/A,Capacity: 64GB, Camera: 8MP back 7MP front,Touch ID fingerprint sensor,Up to 10 hours of battery life ",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Lenovo TAB M7 7 inch 2GB RAM 32GB",
                         Description = "Lenovo TAB M7 Android Tablet comes with MediaTek MT8321 32-bit, Quad-Core (up to 1.30 GHz) Processor, Android 9 Pie Go Edition Operating System. This tablet is featured with 7inch(1024 x 600) IPS,touchscreen,350 nits,2GB LPDDR3 Memory,32GB storage, Non - removable Li - Po 3590 mAh battery.The Tab M7 is more stylish than your average bargain tablet, while giving you peace of mind that it won’t break easily.",
                         Brand = "Lenovo",
                         Price = 177,
                         ProductImgUrl = "Lenovo_TAB_M7.jpg",
                         ProductFeatures = "Model: Lenovo TAB M7,Processor: Mediatek  MT8765B-Wi-Fi/LTE, Quad-Core (up to 1.30 GHz), 7inch(1024 x 600) IPS touchscreen, 2GB LPDDR3, 32GB Storage, Android 9 Pie Go Edition",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Lenovo TAB M8 8inch 2GB RAM 32GB ",
                         Description = "The New Lenovo Tab M8 (2nd Gen) designed with good style and design. This TAB is built in 8.15mm thin, with a metal back cover and rounded edges, this tablet feels great in your hands and exudes elegance. This Tablet brings an 8″ IPS HD (1280 x 800) LCD touchscreen display that uses 82% of the tablet’s surface area for seamless visuals, and a Dolby Audio tuned speaker. This one-two punch of visual and audio pleasure makes streaming your favorite videos and playing mobile games even more enjoyable. This new TAB comes with Android 9 Pie, a quad-core processor, and a battery that provides up to 18 hours of web browsing, the Tab M8 (2nd Gen) is your key to nonstop entertainment.",
                         Brand = "Lenovo",
                         Price = 200,
                         ProductImgUrl = "Lenovo_TAB_M8.jpg",
                         ProductFeatures = "Model: M8, 8″ IPS HD (1280 x 800) LCD Display, 8MP rear & 2MP front Camera, Android v9 Pie operating system, 2GB RAM, 32GB internal memory",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 }
}
                     },
                     new Product
                     {
                         Name = "Huawei Media Pad T3-7 Tablet",
                         Description = "Beautiful 7-inch display delivers a cinematic experience to the palm of your hands, with the sound quality to match. The narrow device bezel gives you more screen area to enjoy a visual media experience, while optimised power consumption options make sure you can enjoy the show for longer. Powerful connectivity is built into this entertainment device, using the combined experience of almost three decades of HUAWEI technology. Browse, talk, experience, for longer, with less likelihood of dropped calls, and faster connection, thanks to HUAWEI optimisation for GSM / WCDMA 3G connectivity.",
                         Brand = "Huawei",
                         Price = 200,
                         ProductImgUrl = "Huawei_Media_Pad_T3_7.jpg",
                         ProductFeatures = "Model: Huawei Media Pad T3-7, Size: 7.0 inches, CPU: Spreadtrum SC7731G quad-core A7 4 x 1.3 GHz, EMOTION UI: EMUI 5.1",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Lenovo Tab 4 8 Plus 3GB RAM, 16GB Storage",
                         Description = "Beautifully designed, powerfully built, and surprisingly rugged, the Tab 4 8 Plus is a great choice for families. It comes with sleek dual-glass casings that help prevent scratches and bumps. It supports multiple accounts so each family member can enjoy their own experience. What’s more, the Tab 4 8 Plus offers an optional pack to transform it into a kid’s tablet. A family tablet has never looked so good. Ultra-slim at 7 mm thick, the light at 300 grams, the Tab 4 8 Plus boasts stylish front-and-back glass panels. But it’s also tough enough for rough handling: Its dual layers of protective glass protect it from scratches and dents",
                         Brand = "Lenovo",
                         Price = 294,
                         ProductImgUrl = "Lenovo_Tab_4_8_Plus.jpg",
                         ProductFeatures = "Model: Lenovo Tab 4 8 Plus, Android 8.1 OREO Version, Pin-sharp resolution, Svelte, light, and robust, Audio that flows around you",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Apple iPad Mini 5 7.9 inch MUXD2 Wi-Fi 256GB",
                         Description = "iPad mini is beloved for its size and capability. And now there are even more reasons to love it. The A12 Bionic chip with Neural Engine. A 7.9 inch Retina display with True Tone. And Apple Pencil, so you can capture your biggest ideas wherever they come to you. It’s still iPad mini. There’s just more of it than ever. iPad mini features a thin, light, and portable design that makes it the perfect on-the-go companion. At 0.66 pound and 6.1 mm thin,1 it’s easy to carry with you in one hand or take out of a pocket or bag whenever inspiration strikes. The A12 Bionic chip with Neural Engine enables a remarkable level of power and intelligence",
                         Brand = "Apple",
                         Price = 976,
                         ProductImgUrl = "iPad_Mini_5_MUXD2.jpg",
                         ProductFeatures = "Model: Apple iPad Mini 5 7.9 inch MUXD2,Capacity: 64GB, Camera: 8MP back 1.2MP front, Touch ID fingerprint sensor, Up to 10 hours of battery life",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Apple iPad Pro 2020 MXDD2ZP/A 11 inch Wi-Fi 256GB",
                         Description = "Apple iPad Pro 11 inch MXDD2ZP/A Wi-Fi 256GB Silver is designed with the iPad Pro's 11inch display features a 2388 x 1668 resolution,ProMotion technology, wide color and True Tone support, as well as a 600 cd / m2 brightness rating.Performance - wise,Apple has replaced the A10X processor with the A12X Bionic chip.This multi - core processor handles all the computational and graphical processing needed for complicated tasks.In this iPad Pro,The Liquid Retina display in the iPad Pro features wide color support,True Tone,and an anti - reflective coating for a natural,accurate viewing experience both indoors and out. and also supports ProMotion technology,which automatically adjusts the display's refresh rate, up to 120Hz, for smooth and responsive scrolling. In addition, it's paired with a Neural Engine chip for advanced machine learning.With the removal of the Home button which also functioned as the fingerprint sensor,the iPad Pro has opted for Face ID via its front - facing 7MP TrueDepth camera.Lastly,Apple has opted for a USB Type - C connector rather than its traditional Lightning connector for additional features and broader compatibility with other devices",
                         Brand = "Apple",
                         Price = 1235,
                         ProductImgUrl = "iPad_Pro_2020_MXDD2ZP_A.jpg",
                         ProductFeatures = "Model: Apple iPad Pro 2020 MXDD2ZP/A 11 inch, A12Z Bionic chip with 64-bit architecture, 11inch Multi - Touch Liquid Retina Display, Camera: 12MP ƒ / 1.8 aperture, Capacity: 256GB",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Apple iPad Pro 2020 MXE42 11 inch Wi-Fi 256GB ",
                         Description = "Apple iPad Pro 11 inch MXE42 Wi-Fi and Cellular 256GB Space Gray is designed with the iPad Pro's 11inch display features a 2388 x 1668 resolution, ProMotion technology, wide color and True Tone support, as well as a 600 cd / m2 brightness rating.Performance - wise, Apple has replaced the A10X processor with the A12X Bionic chip.This multi - core processor handles all the computational and graphical processing needed for complicated tasks.In this iPad Pro, The Liquid Retina display in the iPad Pro features wide color support, True Tone,and an anti - reflective coating for a natural,accurate viewing experience both indoors and out. and also supports ProMotion technology, which automatically adjusts the display refresh rate, up to 120Hz, for smooth and responsive scrolling. In addition, it is paired with a Neural Engine chip for advanced machine learning.With the removal of the Home button which also functioned as the fingerprint sensor, the iPad Pro has opted for Face ID via its front - facing 7MP TrueDepth camera.Lastly Apple has opted for a USB Type - C connector rather than its traditional Lightning connector for additional features and broader compatibility with other devices. ",
                         Brand = "Apple",
                         Price = 1411,
                         ProductImgUrl = "iPad_Pro_2020_MXE42.jpg",
                         ProductFeatures = "Model: Apple iPad Pro 2020 MXE42, A12Z Bionic chip with 64-bit architecture, 11inch Multi - Touch Liquid Retina Display, Camera: 12MP ƒ / 1.8 aperture, Capacity: 256GB",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Lenovo Tab V7 Tablet + Smartphone",
                         Description = "Lenovo TAB V7 is a 6.95 inch IPS FHD(1080 x 2160) 4G Slate Black Tablet which is powered by Qualcomm Octa - Core 450 Processor and Android Pie 9.0 operating system.It also has 4GB Ram and 64GB ROM in this price range. In this tablet, you will also get 13 MP auto focus rear camera, 5.0 MP fixed focus front camera.If we comes to its connectivity, it also has 802.11 b / g / n Wi - Fi and Bluetooth 4.0 connectivity.Thanks to Android Pie because the Lenovo Tab V7's file storage goes twice the distance and the Apps have been optimized with smaller app sizes, and only nine come pre-installed which means less bloat and more storage to enjoy pictures, music, and games. This exclusive Lenovo TAB comes with 01 year warranty.",
                         Brand = "Lenovo",
                         Price = 257,
                         ProductImgUrl = "Lenovo_Tab_V7.jpg",
                         ProductFeatures = "Model: Lenovo Tab V7, 6.95inch IPS FHD(1080 x 2160), 4GB RAM + 64GB ROM, Qualcomm Octa - Core 450 Processor, 5180 mah Battery",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Lenovo Tab 4 10 Plus 4GB RAM, 64GB Storage",
                         Description = "A family tablet has never looked so good. Ultra slim at 7 mm thick, light at 475 grams, the Tab 4 10 Plus boasts stylish front-and-back glass panels. But it’s also tough enough for rough handling: Its dual layers of protective glass protect it from scratches and dents. Beneath the Tab 4 10 Plus’ stylish exterior beats a powerful Qualcomm® Snapdragon™ 64-bit octa-core processor. Backed by up to 4 GB of and 64 GB of storage, this powerful device can seamlessly handle anything you throw at it—and more",
                         Brand = "Lenovo",
                         Price = 518,
                         ProductImgUrl = "Lenovo_Tab_4_10_Plus.jpg",
                         ProductFeatures = "Model: Lenovo Tab 4 10 Plus, Easy sharing with multi-account fingerprint access, Svelte, light, and robust, Dazzling, immersive display, Lots of power, seamless performance",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 }
}
                     },
                     new Product
                     {
                         Name = "Microsoft Surface Pro 7 10th Gen Core i5 8GB Ram 128GB",
                         Description = "More powerful than ever, Surface Pro 7 features a powerful Intel Core processor, improved battery and graphics, and more multitasking connections. Work and play virtually anywhere. With tablet-to-laptop versatility and better connectivity with USB-C and USB-A ports, ultra-slim and light Surface Pro 7 adapts to you. For a full laptop anywhere, personalize Surface Pro 7 with our Signature Type Cover, Surface Pen, and Surface Arc Mouse which are all in rich colors you can mix or match.",
                         Brand = "Microsoft",
                         Price = 1117,
                         ProductImgUrl = "Surface_Pro_7.jpg",
                         ProductFeatures = "Model: Microsoft Surface Pro 7, Intel Core i5-1035G4 Processor (6 MB Cache up to 3.70 GHz), Screen: 12.3” PixelSense Display, Resolution: 2736 x 1824 (267 PPI), 8GB RAM & 128GB SSD",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Microsoft Surface Pro 7 10th Gen Core i5 128GB",
                         Description = "Microsoft Surface Pro 7 comes with Type Cover Keyboar. It is more powerful than ever, Surface Pro 7 features a powerful Intel Core i5-1035G4 Processor (6 MB Cache, 1.10 GHz up to 3.70 GHz), improved battery (Up to 10.5 hours of typical device usage) and Intel Iris Plus Graphics, and more multitasking connections. Work and play virtually anywhere. With tablet-to-laptop versatility and better connectivity with USB-C and USB-A ports, ultra-slim and light Surface Pro 7 adapts to you. This Microsoft Surface Pro 7 has 8GB LPDDR4x RAM, 128GB SSD with Windows 10. ",
                         Brand = "Microsoft",
                         Price = 1352,
                         ProductImgUrl = "Surface_Pro_7_Touch_Display.jpg",
                         ProductFeatures = "Model: Microsoft Surface Pro 7, Intel Core i5-1035G4 Processor (6 MB Cache 1.10GHz up to 3.70 GHz), 12.3” (2736x1824) PixelSense Display, 8GB RAM & 128GB SSD, With Type Cover Keyboard",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Apple iPad 10.2 Inch MW792 7th Gen Wi-Fi, 128GB, Gold",
                         Description = "In this Apple iPad, the 10.2inch  LED - backlit multi - touch display has a 2160 x 1620 resolution for 264 pixels per inch and a total of over 3 million pixels.The display also features an oleophobic coating, which helps to reduce fingerprints and smudges and making it an ideal companion for watching movies, creating content, and much more.Powered by the A10 Fusion chip, it will enable users to edit 4K video, play graphics - intensive games, and experience AR apps.Moreover, its Smart Connector offers Smart Keyboard support for comfortable typing, while its display is compatible with the 1st gen Apple Pencil, so users can write and draw.With front and back cameras, the iPad can take photos, capture video, scan documents, make FaceTime calls, and provide AR capabilities.This iPad is connectivity - wise, it's equipped with Wi-Fi 5 (802.11ac), Bluetooth 4.2, a Lightning connector, and a 3.5mm audio jack.",
                         Brand = "Apple",
                         Price = 612,
                         ProductImgUrl = "iPad_MW792.jpg",
                         ProductFeatures = "Model: Apple 10.2 Inch MW792 128GB iPad, Capacity: 128GB, Camera: 8MP back 1.2MP front, Touch ID fingerprint sensor, Up to 10 hours of battery life",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Lenovo TAB 4 10 2GB Ram 16GB Storage 10 inch",
                         Description = "Designed with cord-cutters in mind, the Tab 4 10 is great for kicking back to watch your favorite TV shows on Amazon, Hulu, or Netflix. Its vibrant 10.1” HD display boasts 1280x800 resolution and wide-angle views. The result? A great visual experience. Beneath the stylish exterior of the Tab 4 10 beats a powerful Qualcomm® Snapdragon™ 64-bit quad-core processor. Backed by up to 2 GB of RAM and 16 GB of storage, this tablet can seamlessly handle anything you throw at it—and more.",
                         Brand = "Lenovo",
                         Price = 266,
                         ProductImgUrl = "Lenovo_TAB_4_10.jpg",
                         ProductFeatures = "Model: Lenovo TAB 4 10, APQ8017 Processor (1.40GHz), Capacity 16 GB, 2GB LPDDR3 Ram, 10.1 inch(1280 x 800) LCD IPS Multitouch",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 }
}
                     },
                     new Product
                     {
                         Name = "Lenovo Tab M10 2GB RAM 16GB Storage",
                         Description = "Treat your eyes to something truly special. The Tab M10's 10.1 inch HD display captures everything from the intensity of an action scene to the beautiful details in family photos.At just 8.1 mm thin and 480 g, the Tab M10 is a delight to hold and fits easily into your satchel or backpack.It's built to be taken anywhere without weighing you down. Immerse yourself in booming sound when you enjoy music, movies, shows, and videos. Its dual front speakers and Dolby Atmos support mean you can enjoy a theater-like audio experience wherever you go. Add in the optional Kid’s Pack to keep your children—and your device—safe, thanks to a custom shock-resistant bumper and special kid’s mode that features up-to-date and curated children’s content.",
                         Brand = "Lenovo",
                         Price = 259,
                         ProductImgUrl = "Lenovo_Tab_M10.jpg",
                         ProductFeatures = "Model: Lenovo Tab M10, CPU Type: Qualcomm Snapdragon 429, Resolution: 1280 x 800 pixels, 2 GB RAM, 16 GB ROM, Dolby Atmos, Non removable Li-ion 4850 mAh Battery",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     }
                     /* New Tablet Data Ends Here */





                     );
                context.SaveChanges();
            }
        }
    }
}
