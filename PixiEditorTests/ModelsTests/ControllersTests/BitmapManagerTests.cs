﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Media;
using PixiEditor;
using PixiEditor.Models.Controllers;
using PixiEditor.Models.DataHolders;
using PixiEditor.Models.Layers;
using PixiEditor.Models.Position;
using PixiEditor.Models.Tools;
using PixiEditor.Models.Tools.Tools;
using Xunit;

namespace PixiEditorTests.ModelsTests.ControllersTests
{
    public class BitmapManagerTests
    {

        [Fact]
        public void TestThatBitmapManagerSetsCorrectTool()
        {
             BitmapManager bitmapManager = new BitmapManager();
             bitmapManager.SetActiveTool(new MockedPen());
             Assert.Equal(ToolType.Pen, bitmapManager.SelectedTool.ToolType);
        }

        [Fact]
        public void TestThatBitmapManagerAddsEmptyNewLayer()
        {
            string layerName = "TestLayer";
            BitmapManager bitmapManager = new BitmapManager
            {
                ActiveDocument = new Document(10, 10)
            };
            bitmapManager.AddNewLayer(layerName);
            Assert.Single(bitmapManager.ActiveDocument.Layers);
            Assert.Equal(layerName, bitmapManager.ActiveDocument.ActiveLayer.Name);
            Assert.Equal(0, bitmapManager.ActiveDocument.ActiveLayer.Width + bitmapManager.ActiveDocument.ActiveLayer.Height);
        }

        [Fact]
        public void TestThatBitmapManagerRemovesLayer()
        {
            BitmapManager bitmapManager = new BitmapManager
            {
                ActiveDocument = new Document(10, 10)
            };
            bitmapManager.AddNewLayer("_");
            bitmapManager.AddNewLayer("_1");
            Assert.Equal(2,bitmapManager.ActiveDocument.Layers.Count);
            bitmapManager.RemoveLayer(0);
            Assert.Single(bitmapManager.ActiveDocument.Layers);
        }

        [Fact]
        public void TestThatGeneratePreviewLayerGeneratesPreviewLayer()
        {
            BitmapManager bitmapManager = new BitmapManager
            {
                ActiveDocument = new Document(10, 10)
            };
            bitmapManager.GeneratePreviewLayer();
            Assert.NotNull(bitmapManager.PreviewLayer);
            Assert.Equal(0, bitmapManager.PreviewLayer.Width + bitmapManager.PreviewLayer.Height); //Size is zero
            Assert.Equal(0, bitmapManager.PreviewLayer.OffsetX + bitmapManager.PreviewLayer.OffsetY); //Offset is zero
            Assert.Equal(bitmapManager.ActiveDocument.Width,bitmapManager.PreviewLayer.MaxWidth);
            Assert.Equal(bitmapManager.ActiveDocument.Height,bitmapManager.PreviewLayer.MaxHeight);
        }

        [Fact]
        public void TestThatIsOperationToolWorks()
        {
            MockedPen pen = new MockedPen();
            Assert.True(BitmapManager.IsOperationTool(pen));
        }

    }

    public class MockedPen : BitmapOperationTool
    {
        public override LayerChange[] Use(Layer layer, Coordinates[] mouseMove, Color color)
        {
            PenTool pen = new PenTool()
            {
                Toolbar = null
            };
           return pen.Use(layer, mouseMove, color);
        }

        public override ToolType ToolType { get; } = ToolType.Pen;
    }
}