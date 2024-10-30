// MonoGame - Copyright (C) MonoGame Foundation, Inc
// This file is subject to the terms and conditions defined in
// file 'LICENSE.txt', which is part of this source code package.

using Microsoft.Xna.Framework.Graphics;

namespace Microsoft.Xna.Framework
{
    public partial class GraphicsDeviceManager
    {
        partial void PlatformInitialize(PresentationParameters presentationParameters)
        {
            var surfaceFormat = _game.graphicsDeviceManager.PreferredBackBufferFormat.GetColorFormat();
            var depthStencilFormat = _game.graphicsDeviceManager.PreferredDepthStencilFormat;

            // TODO Need to get this data from the Presentation Parameters
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.RedSize, surfaceFormat.R);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.GreenSize, surfaceFormat.G);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.BlueSize, surfaceFormat.B);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.AlphaSize, surfaceFormat.A);

            switch (depthStencilFormat)
            {
                case DepthFormat.None:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 0);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth16:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 16);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth24:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 24);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 0);
                    break;
                case DepthFormat.Depth24Stencil8:
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.DepthSize, 24);
                    Sdl.GL.SetAttribute(Sdl.GL.Attribute.StencilSize, 8);
                    break;
            }

            Sdl.GL.SetAttribute(Sdl.GL.Attribute.DoubleBuffer, 1);

            // 4.6 Compliance
            // Change the major and minor versions to match target (4.6)

            Sdl.GL.SetAttribute(Sdl.GL.Attribute.ContextMajorVersion, 4);
            Sdl.GL.SetAttribute(Sdl.GL.Attribute.ContextMinorVersion, 6);

            if (presentationParameters.MultiSampleCount > 0)
            {
                Sdl.GL.SetAttribute(Sdl.GL.Attribute.MultiSampleBuffers, 1);
                Sdl.GL.SetAttribute(Sdl.GL.Attribute.MultiSampleSamples, presentationParameters.MultiSampleCount);
            }

            ((SdlGameWindow)SdlGameWindow.Instance).CreateWindow();
        }
    }
}
