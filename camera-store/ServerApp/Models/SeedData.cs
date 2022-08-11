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
                        Name = "Canon EOS 3000D DSLR Camera",
                        Description = "EOS 3000D comes with 230,000 dot 3.0 inch LCD, 3 frames per second shooting to catch action sequences. Includes 3x 18-55mm Zoom-NIKKOR VR Image Stabilization Lens. Ultrasonic process and exclusive Airflow Control combats the accumulation of dust in front of the image sensor, safeguarding image quality shot after shot. Active D-Lighting : Restores picture-enhancing detail in shadows and highlights. Split-second Shutter Response Eliminates the frustration of shutter delay, capturing moments that other cameras miss.",
                        Brand = "Canon",
                        Price = 380,
                        ProductImgUrl = "EOS_3000d.jpg",
                        ProductFeatures = "Model: Canon EOS 3000D,Type CMOS sensor,Effective Pixels 24.1 Megapixels,Screen Type 6.8 cm TFT LCD,DIGIC 4+ image Processor",
                        Supplier = s1,
                        Ratings = new List<Rating> {
                             new Rating { Stars = 4 }, new Rating { Stars = 3 }}
                    },
                     new Product
                     {
                         Name = "Canon EOS 80D DSLR Camera",
                         Description = "Whether raising your game to SLR level photography or having fun with a feature-rich, versatile SLR you can use pretty much anywhere, the EOS 80D camera is your answer. It features an impressive 45-point all cross-type AF system* that provides high-speed, highly precise AF in virtually any kind of light. To help ensure photographers don't miss their shot, an Intelligent Viewfinder with approximately 100% coverage provides a clear view and comprehensive image data. Improvements like a powerful 24.2 Megapixel (APS-C) CMOS sensor and Dual Pixel CMOS AF for Live View shooting enhance the EOS 80D's performance across the board. Complementing the EOS 80D's advanced operation are built-in wireless connectivity and Full HD 60p movies that can be saved as MP4s for easy sharing. Merging power, precision and operability, the EOS 80D is a dynamic SLR camera for anyone ready to realize their creative vision.",
                         Brand = "Canon",
                         Price = 1120,
                         ProductImgUrl = "EOS_80d.jpg",
                         ProductFeatures = "Model: Canon EOS 80D,24MP APS-C CMOS sensor,DIGIC 6 Image Processor,Full HD 1080/60p,7 fps continuous shooting with AF",
                         Supplier = s1,
                         Ratings = new List<Rating> {
                             new Rating { Stars = 2 }, new Rating { Stars = 5 }}
                     },
                     new Product
                     {
                         Name = "CANON EOS 200D II 24.1 MP",
                         Description = "EOS 200D II is Canon’s lightest DSLR with a Vari-angle Touch Screen LCD. Weighing only a little heavier than a bottle of water*, the camera slides right into your bag conveniently for that everyday photography. Packed into its petite body is Canon’s 24.1-megapixels APS-C CMOS sensor, DIGIC 8 processor and a bunch of features that would make snapping your everyday life seamlessly easier. First time incorporated into an EOS DLSR, Creative Assist and Smooth Skin feature would allow you to achieve desired effects on your photos and easily take picture perfect selfies. Stay connected to the camera with the Bluetooth Low Energy connection and send images to your mobile devices via Wi-Fi as you shoot for ease of sharing. The EOS 200D II is designed for comfort with its deep grip and ergonomically laid out function dials. With Black, Silver and White to choose from, it is sure to adds a statement to your lifestyle.Dual Pixel CMOS AF, Canon’s distinctive technology in which effective pixel on the CMOS sensor is able to perform both phase-difference autofocusing and imaging functions. Through this, EOS 200D II can achieve 4,779 selectable AF positions (during Live View) that allows greater flexibility when composing an image. EOS 200D II inherits the Eye Detection AF, which can detect and focus on the subject’s pupil. Especially useful when shooting wide open on large aperture lens, always ensuring that the eye is in focus—ideal for portrait shooting. This function is available in Servo mode for both stills and movie. Capturing candid genuine expression of someone dancing or your children running in circles is now made easier. Your everyday photography is now made easier with Creative Assist. You just have to select you desired effect and the camera would provide the most appropriate setting to achieve that image for you. Creative Filters like miniature effect or fish-eye effect would certainly make your image pop. EOS 200D II have Skin Smoothing for those who enjoys taking that picture-perfect selfies. You will receive 03 Years Service Warranty (No parts warranty).",
                         Brand = "Canon",
                         Price = 680,
                         ProductImgUrl = "EOS_200d.jpg",
                         ProductFeatures = "Model: Canon 200D II,Dual Pixel CMOS AF,24.1MP APS-C CMOS Sensor,3,975 selectable focus positions,Creative Assist, Creative Filters and Smooth Skin",
                         Supplier = s2,
                         Ratings = new List<Rating> {
                             new Rating { Stars = 1 }, new Rating { Stars = 3 }}
                     },
                     new Product
                     {
                         Name = "Canon EOS RP Mirrorless Digital Camera",
                         Description = "For all their appealing features one of the concerns of using a full-frame camera has been its larger profile, however, the EOS RP is the lightest and smallest Canon EOS full-frame camera to date. * Weighing approx. 440g (body only) while being impressively compact and comfortable to use, the EOS RP is more convenient to carry, shoot with, and store than any previous EOS full-frame camera before. Engineered to work seamlessly with RF lenses, the EOS RP also maintains complete compatibility with EF and EF-S lenses by using one of three optional Mount Adapters. When using EF-S lenses, the EOS RP even crops automatically to reflect the APS-C sized sensor the lenses are designed for. The EOS RP camera features a Canon-developed and produced 35mm full-frame CMOS sensor with approx. 26.2 effective megapixels. It employs the DIGIC 8 Image Processor to enhance the speed of operations across the board. This means fast, efficient performance and phenomenal image quality for big prints. The EOS RP camera's highly responsive Dual Pixel CMOS AF system features 4,779 manually selectable AF points** and can deliver sharp focus within approximately 0.05 seconds***. ",
                         Brand = "Canon",
                         Price = 2300,
                         ProductImgUrl = "EOS-RP.jpg",
                         ProductFeatures = "Model: Canon EOS RP,26.2 megapixel,Aspect Ratio 3:2,CMOS sensor (compatible with Dual Pixel CMOS AF),7.5 cm TFT LCD",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Canon EOS 5D Mark IV DSLR Camera",
                         Description = "Canon EOS 5D Mark IV DSLR Camera (Only Body) which is an outstanding still photography option and an able 4K-capable video machine. This multimedia maven offers a newly developed 30.4MP full-frame CMOS sensor paired with the DIGIC 6+ image processor in order to balance fine detail and resolution with low-light performance and sensitivity. It is able to work within a native range of ISO 100-32000, which can then be expanded to an impressive ISO 50-102400, for sharp, low-noise images in a variety of conditions. Along with these improvements to image quality, users will enjoy a performance boost across the board with an enhanced AF system, built-in Wi-Fi, NFC, and GPS, and much more",
                         Brand = "Canon",
                         Price = 2150,
                         ProductImgUrl = "EOS_5D_Mark_IV.jpg",
                         ProductFeatures = "Model: Canon EOS 5D Mark IV,30.4MP Full-Frame CMOS Sensor,DIGIC 6+ Image Processor,DCI 4K Video at 30 fps,7 fps Shooting; CF & SD Card Slots",
                         Supplier = s2,
                         Ratings = new List<Rating> { new Rating { Stars = 1 },
                             new Rating { Stars = 4 }, new Rating { Stars = 3 }}
                     },
                     new Product
                     {
                         Name = "Nikon D3200 DSLR 24.2 MP",
                         Description = "Nikon D3200 digital SLR camera with 18-55mm lens kit. Don’t let the D3200’s compact size and price fool you —packed inside this easy to use HD-SLR is serious Nikon power: a 24.2MP DX-format CMOS sensor that excels in any light, Expeed 3 image-processing for fast operation and creative in-camera effects, Full HD (1080p) movie recording, in-camera tutorials and much more. What does this mean for you? Simply stunning photos and videos in any setting. And now, with Nikon’s optional Wireless Mobile Adapter, you can share those masterpieces instantly with your Smartphone or tablet easily.",
                         Brand = "Nikon",
                         Price = 330,
                         ProductImgUrl = "D3200.jpg",
                         ProductFeatures = "Model: Nikon D3200 DSLR,CMOS DX-format sensor,24.2 Megapixels,4 FPS Continuous Shooting,100-6400 ISO Expandable to 12800",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 5 },
                             new Rating { Stars = 4 }}
                     },
                     new Product
                     {
                         Name = "Nikon D5300 DSLR 24.2 MP",
                         Description = "Nikon D5300 DSLR 24.2 MP Builtin Wi-Fi With 18-55mm Lens. Nikon D5300's photos and Full HD videos is nothing short of astounding. A recent design innovation allows the D5300's 24.2-megapixel DX format CMOS image sensor* to capture pure, lifelike images. Enlarge or crop your photos without losing any sharpness or detail—a feat not possible with most smartphones. Pair that capability with any exceptional NIKKOR lens, marvels of clarity and sharpness in their own right, and you'll experience the image quality your memories deserve.",
                         Brand = "Nikon",
                         Price = 490,
                         ProductImgUrl = "D5300.jpg",
                         ProductFeatures = "Model: Nikon D5300 DSLR,DX-format CMOS sensor,24.2 Megapixels;3.2\"Display,5 FPS Continuous Shooting,Full HD 1080p at 60/50/30/25/24p",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 4 } }
                     },
                     new Product
                     {
                         Name = "Nikon D5500 DSLR 24.2 MP",
                         Description = "Nikon D5500, Effective pixels - 24.2 Mega Pixel, Lens Mount - AF-S 18-55mm VR, Sensor Type - CMOS, Display - 3.2\" Touch LCD Display, View Finder Type - Pentamirror, Shutter Speed - 1/4000 to 30 sec., Face Detection - Yes, Red-Eye Reduction - Yes, Image - 6000 x 4000, Video - 1920 x 1080, Memory Type - SD, SDHC, SDXC, USB - Hi-Speed USB, Battery - Lithium-ion Battery EN-EL14a, Body Dimensions - 124 x 97 x 70 mm, Weight - 470gm.",
                         Brand = "Nikon",
                         Price = 485,
                         ProductImgUrl = "D5500.jpg",
                         ProductFeatures = "Model: Nikon D5500 Digital SLR Camera,DX-format CMOS sensor,24.2 Megapixel,5 FPS Continuous Shooting,Full HD 1080p at 60/50/30/25/24p",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Nikon Z6 24.5MP Mirrorless",
                         Description = "Bring your ideas into the light. When you have a fast full-frame sensor to shoot with you have the power to make your mark. The back-illuminated 24.5 MP full-frame CMOS sensor with focal-plane AF captures richly detailed, razor-sharp images. The blazing EXPEED 6 image processor delivers images with low noise and stunning dynamic range at both high and low ISO values. The wide 55 mm mount diameter means your Z series camera takes in more light.",
                         Brand = "Nikon",
                         Price = 2661,
                         ProductImgUrl = "z-6-lens.jpg",
                         ProductFeatures = "Model: Nikon Z6,24.3MP FX-Format CMOS Sensor,EXPEED 4 Image Processor,FTZ Adapter is given with the camera,Full HD 1080p Video Recording",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 5 } }
                     },
                     new Product
                     {
                         Name = "NIKON Z50 Mirrorless",
                         Description = "NIKON Z50 Mirrorless Digital Camera is a DX-format mirrorless digital camera revolving around the versatile Z Mount. It is Capable in both stills and video realms, the Z50 features a 20.9MP CMOS sensor and EXPEED 6 image processor, which enables fast performance up to 11 fps, a reliable 209-point hybrid AF system with eye detection and notable low-light performance to ISO 51200. For video shooters, UHD 4K is supported up to 30 fps along with Full HD recording at up to 120 fps for slow-motion playback.",
                         Brand = "Nikon",
                         Price = 1123,
                         ProductImgUrl = "z50-mirrorless.jpg",
                         ProductFeatures = "Model: Z50 Mirrorless,20.9MP DX-Format CMOS Sensor,EXPEED 6 Image Processor,UHD 4K and Full HD Video Recording,2.36m-Dot OLED Electronic Viewfinder",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 5 } }
                     },
                     new Product
                     {
                         Name = "Sony Alpha A6000 Mirrorless",
                         Description = "Unlike many competitors, the α6000’s Fast Hybrid AF combines the strengths of both phase- and contrast-detection autofocus. With a class-leading 179 phase detection points (covering almost the entire image) and a high-speed contrast-detection function, the result is not only an impressive 11 fps burst mode, but also highly accurate movement tracking for both stills and video. 4D FOCUS enables superior autofocus performance in four dimensions.",
                         Brand = "Sony",
                         Price = 506,
                         ProductImgUrl = "Alpha_A6000.jpg",
                         ProductFeatures = "Model: Sony Alpha A6000,24.3-megapixel APS-C image sensor,BIONZ X image processing engine,One-touch remote and sharing,High-resolution OLED Tru-Finder",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 4 } }
                     },
                     new Product
                     {
                         Name = "Sony Alpha A6400 Mirrorless",
                         Description = "Sony Alpha A6400 Mirrorless Digital Camera with 16-50mm Lens is comes with 24.2MP APS-C Exmor CMOS Sensor, BIONZ X Image Processor, Real-Time Eye AF & Real-Time Tracking XGA Tru-Finder 2.36m-Dot OLED EVF. This camera is stable, fast, versatile, and compact, the Alpha a6400 from Sony is an APS-C-format mirrorless camera that adopts many of the features normally reserved for their full-frame lineup.",
                         Brand = "Sony",
                         Price = 1060,
                         ProductImgUrl = "Alpha_A6400.jpg",
                         ProductFeatures = "Model: Sony Alpha A6400,24.2MP APS-C Exmor CMOS Sensor,BIONZ X Image Processor,Real-Time Eye AF; Real-Time Tracking,XGA Tru-Finder 2.36m-Dot OLED EVF",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 4 } }
                     },
                     new Product
                     {
                         Name = "Panasonic Lumix G7 16MP 4K",
                         Description = "16 MP Live MOS Micro Four Thirds sensor pairs with an updated Venus Engine 9 to deliver fast overall performance with matched image quality to suit working in a wide variety of lighting conditions with consistent results. Sensitivity ranges from ISO 200 to 25600, with the ability to extend down to ISO 100 for working in bright conditions or with longer shutter speeds. In addition to the versatile still shooting modes, the G7 also supports recording 4K UHD (3840 x 2160) video with either 30p or 24p frames rates at 100Mbps in the MP4 format.",
                         Brand = "Panasonic",
                         Price = 659,
                         ProductImgUrl = "Lumix_G7.jpg",
                         ProductFeatures = "Model: Panasonic Lumix G7,16MP Digital Live MOS Sensor & Venus Engine,Micro Four Thirds sensor,Dual I.S. 2,Wi-Fi + Bluetooth",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 3 } }
                     },
                     new Product
                     {
                         Name = "Panasonic Lumix G85 16MP 4K",
                         Description = "The G85 is capable of capturing clear, high-resolution stills as well as UHD 4K video, with help of a 16MP Live MOS sensor. The sensor's design omits an optical low-pass filter in order to achieve a high degree of sharpness and resolution, and its design also owns a sensitivity range from ISO 200-25600 to suit working in a variety of lighting conditions. g85 also own a built-in stereo microphone.",
                         Brand = "Panasonic",
                         Price = 942,
                         ProductImgUrl = "Lumix_G85.jpg",
                         ProductFeatures = "Model: Panasonic Lumix G85,16MP Digital Live MOS Sensor & Venus Engine,5-Axis Gyro Sensor Compensation,Shutter Shock Reduction,Dual I.S. Effective To 5 Stops",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 4 } }
                     },
                     new Product
                     {
                         Name = "Panasonic Lumix GH5 20.3MP 4K",
                         Description = "Recently developed 20.3MP Digital Live MOS sensor without an optical low-pass filter and a new Venus Engine processor, the GH5 claims to produce the best image quality to date from a Lumix camera. The GH5 is an exceptionally capable video camera in addition to its outstanding stills capabilities. The flagship feature is UHD 4K video at up to 60 fps, though another excellent spec is the ability to record 4:2:2 10-bit files at resolutions up to DCI and UHD 4K at 24/30 fps.",
                         Brand = "Panasonic",
                         Price = 1414,
                         ProductImgUrl = "Lumix_GH5.jpg",
                         ProductFeatures = "Model: Panasonic Lumix GH5,20.3MP Digital Live MOS Sensor & Venus Engine,0.3MP Micro Four Thirds sensor,Dual I.S. 2,Wi-Fi + Bluetooth",
                         Supplier = s3,
                         Ratings = new List<Rating> { new Rating { Stars = 5 } }
                     });
                context.SaveChanges();
            }
        }
    }
}
