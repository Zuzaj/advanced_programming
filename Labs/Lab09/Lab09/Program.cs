using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;

class Program  
{  
            public static Rgba32 medianFilter(Image<Rgba32> image, int x, int y, int width, int height)
        {
            List<byte> R = new List<byte>();
            List<byte> G = new List<byte>();
            List<byte> B = new List<byte>();

            int widthRadius = width / 2;
            int heightRadius = height / 2;
            for (int a = x - widthRadius; a <= x + widthRadius; a++)
                for (int b = y - heightRadius; b <= y + heightRadius; b++)
                {
   
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }

            R.Sort();
            G.Sort();
            B.Sort();

            return new Rgba32(R[R.Count / 2], G[G.Count / 2], B[B.Count / 2]);
        }

        public static Rgba32 minFilter(Image<Rgba32> image, int x, int y, int width, int height)
        {
            List<byte> R = new List<byte>();
            List<byte> G = new List<byte>();
            List<byte> B = new List<byte>();

            int widthRadius = width / 2;
            int heightRadius = height / 2;
            for (int a = x - widthRadius; a <= x + widthRadius; a++)
                for (int b = y - heightRadius; b <= y + heightRadius; b++)
                {
   
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }

            R.Sort();
            G.Sort();
            B.Sort();

            return new Rgba32(R[0], G[0], B[0]);
        }
        public static Rgba32 maxFilter(Image<Rgba32> image, int x, int y, int width, int height)
        {
            List<byte> R = new List<byte>();
            List<byte> G = new List<byte>();
            List<byte> B = new List<byte>();

            int widthRadius = width / 2;
            int heightRadius = height / 2;
            for (int a = x - widthRadius; a <= x + widthRadius; a++)
                for (int b = y - heightRadius; b <= y + heightRadius; b++)
                {
   
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }

            R.Sort();
            G.Sort();
            B.Sort();

            return new Rgba32(R[R.Count-1], G[G.Count-1], B[B.Count-1]);
        }
        public static Rgba32 avgFilter(Image<Rgba32> image, int x, int y, int width, int height)
        {
            List<int> R = new List<int>();
            List<int> G = new List<int>();
            List<int> B = new List<int>();

            int widthRadius = width / 2;
            int heightRadius = height / 2;
            for (int a = x - widthRadius; a <= x + widthRadius; a++)
                for (int b = y - heightRadius; b <= y + heightRadius; b++)
                {
   
                    if (a >= 0 && a < image.Width && b >= 0 && b < image.Height)
                    {
                        R.Add(image[a, b].R);
                        G.Add(image[a, b].G);
                        B.Add(image[a, b].B);
                    }
                }

            return new Rgba32((byte) R.Average(), (byte) G.Average(), (byte) B.Average());
        }

        public static Rgba32 convolution(Image<Rgba32> image, int x, int y, List<List<int>> kernel){
            int kernelWidth = kernel[0].Count;
            int kernelHeight = kernel.Count;
            int Rsum = 0;
            int Gsum = 0;
            int Bsum = 0;
            for (int i = x - kernelWidth/2; i<x+kernelWidth/2; i++){
                for(int j = y - kernelHeight/2; j<y+kernelHeight/2; j++){
                   if (i >= 0 && i < image.Width && j >= 0 && j < image.Height){
                        Rsum += image[i, j].R * kernel[i - x + kernelWidth / 2][j - y + kernelHeight / 2];
                        Gsum += image[i, j].G * kernel[i - x + kernelWidth / 2][j - y + kernelHeight / 2];
                        Bsum += image[i, j].B * kernel[i - x + kernelWidth / 2][j - y + kernelHeight / 2];
                   }
                }
            }
            Rsum = Math.Clamp(Rsum, 0, 255);
            Gsum = Math.Clamp(Gsum, 0, 255);
            Bsum = Math.Clamp(Bsum, 0, 255);

            return new Rgba32((byte)Rsum, (byte)Gsum, (byte)Bsum);
        } 
    public static void Task_1(string imageFile){

        using (Image<Rgb24> image = Image.Load<Rgb24>(imageFile)) 
    {
    // klon obrazka - pracujemy teraz na kopii danych
    Image<Rgb24>clone = image.Clone();
    // pętla po wszystkich pikselach
    for (int a = 0; a < image.Width; a++)
        for (int b = 0; b < image.Height; b++)
        {
            byte R = image[a,b].R;
            byte G = image[a,b].G;
            byte B = image[a,b].B;

            byte avg = (byte)(R/3 + G/3 + B/3);
            clone[a,b] = new Rgb24(avg, avg, avg);
        }
    //zapisanie obrazków
    image.Save("kot_kolor.png");
    clone.Save("kot_szary.png");           
    }

    }

    public static void Task_2(string imageFile, int Width, int Height){
        if (Width % 2 == 0 || Height % 2 == 0)
            throw new ArgumentException("Width and height must be odd numbers.");
        using (Image<Rgba32> image = Image.Load<Rgba32>(imageFile)) 
        {
            using var result = new Image<Rgba32>(image.Width, image.Height);
            for (int x = 0; x < image.Width;x++){
                for (int y = 0; y < image.Height;y++){
                    result[x,y] = medianFilter(image, x, y, Width, Height);
                }
            }
   
        result.Save("kot_median.png");           
        }
    }

        public static void Task_3(string imageFile, int Width, int Height){
        if (Width % 2 == 0 || Height % 2 == 0)
            throw new ArgumentException("Width and height must be odd numbers.");
        using (Image<Rgba32> image = Image.Load<Rgba32>(imageFile)) 
        {
            using var resultMin = new Image<Rgba32>(image.Width, image.Height);
            var resultMax = new Image<Rgba32>(image.Width, image.Height);
            var resultAvg = new Image<Rgba32>(image.Width, image.Height);
            for (int x = 0; x < image.Width;x++){
                for (int y = 0; y < image.Height;y++){
                    resultMin[x,y] = minFilter(image, x, y, Width, Height);
                    resultMax[x,y] = maxFilter(image, x, y, Width, Height);
                    resultAvg[x,y] = avgFilter(image, x, y, Width, Height);
                }
            }
   
        resultMin.Save("kot_min.png");   
        resultMax.Save("kot_max.png");  
        resultAvg.Save("kot_avg.png");          
        }
    }

    public static void Task_4(string imageFile, List<List<int>> kernel){
        if (kernel.Count % 2 == 0 || kernel[0].Count % 2 == 0)
            throw new ArgumentException("Kernel dimensions must be odd numbers.");
        using (Image<Rgba32> image = Image.Load<Rgba32>(imageFile)) 
        {
            using var resultConvolution = new Image<Rgba32>(image.Width, image.Height);
            for (int x = 0; x < image.Width;x++){
                for (int y = 0; y < image.Height;y++){
                    resultConvolution[x,y] = convolution(image, x, y, kernel);

                }
            }
        resultConvolution.Save("kot_convolution.png");           
        }
    }
        



    static void Main()  
    {  
        Task_1("kot.png");
        Task_2("kot.png", 3,3);
        Task_3("kot.png", 3, 3);
        List<int> row1 = new(){-1,-1,-1};
        List<int> row2 = new(){-1,8,-1};
        List<List<int>> kernel = new() {row1, row2, row1};
        Task_4("kot.png", kernel);

    }
}
