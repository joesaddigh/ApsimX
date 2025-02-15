using System;
using NUnit.Framework;
using System.Linq;
using System.Text;
using APSIM.Interop.Documentation;
using APSIM.Interop.Markdown.Renderers;
using APSIM.Shared.Documentation;
using System.Collections.Generic;
using APSIM.Interop.Documentation.Renderers;
using TextStyle = APSIM.Interop.Markdown.TextStyle;
using Document = MigraDocCore.DocumentObjectModel.Document;
using Paragraph = APSIM.Shared.Documentation.Paragraph;
using MigraDocParagraph = MigraDocCore.DocumentObjectModel.Paragraph;
using Section = APSIM.Shared.Documentation.Section;
using APSIM.Interop.Documentation.Extensions;
using MigraDocCore.DocumentObjectModel;

namespace UnitTests.Interop.Documentation.TagRenderers
{
    /// <summary>
    /// Tests for the <see cref="ParagraphTagRenderer"/> class.
    /// </summary>
    /// <remarks>
    /// todo: mock out PdfBuilder API.
    /// </remarks>
    [TestFixture]
    public class ParagraphTagRendererTests
    {
        private PdfBuilder pdfBuilder;
        private Document document;
        private ParagraphTagRenderer renderer;

        [SetUp]
        public void SetUp()
        {
            document = new MigraDocCore.DocumentObjectModel.Document();
            // Workaround for a quirk in the migradoc API.
            _ = document.AddSection().Elements;
            pdfBuilder = new PdfBuilder(document, PdfOptions.Default);
            pdfBuilder.UseTagRenderer(new MockTagRenderer());
            renderer = new ParagraphTagRenderer();
        }

        /// <summary>
        /// Paragraphs with no text should not be written to the document.
        /// </summary>
        [TestCase("")]
        [TestCase(null)]
        public void EnsureEmptyParagraphNotWritten(string empty)
        {
            renderer.Render(new Paragraph(empty), pdfBuilder);
            Assert.AreEqual(0, document.LastSection.Elements.Count);
        }

        /// <summary>
        /// Ensure that the paragraph's contents are parsed as markdown.
        /// </summary>
        [Test]
        public void EnsureMarkdownInParagraphIsParsed()
        {
            // Add a paragraph with some italicised text.
            string rawText = "italic text";
            string italics = $"*{rawText}*";
            renderer.Render(new Paragraph(italics), pdfBuilder);

            // Ensure that the paragraph was inserted.
            Assert.AreEqual(1, document.LastSection.Elements.Count);
            MigraDocParagraph paragraph = document.LastSection.Elements[0] as MigraDocParagraph;
            Assert.NotNull(paragraph);
            Assert.AreEqual(1, paragraph.Elements.Count);
            FormattedText insertedText = paragraph.Elements[0] as FormattedText;
            Assert.NotNull(insertedText);

            // Ensure that the inserted raw text matches and is italic.
            Assert.AreEqual(rawText, ((Text)insertedText.Elements[0]).Content);
            Assert.True(document.Styles[insertedText.Style].Font.Italic);
        }

        /// <summary>
        /// Ensure that a paragarph without markdown is inserted correctly.
        /// This should be unnecessary if the markdown tag renderers are
        /// tested correctly.
        /// </summary>
        [Test]
        public void TestInsertWithoutMarkdown()
        {
            // Add a paragraph with some italicised text.
            string rawText = "raw text";
            renderer.Render(new Paragraph(rawText), pdfBuilder);

            // Ensure that the paragraph was inserted.
            Assert.AreEqual(1, document.LastSection.Elements.Count);
            MigraDocParagraph paragraph = document.LastSection.Elements[0] as MigraDocParagraph;
            Assert.NotNull(paragraph);
            Assert.AreEqual(1, paragraph.Elements.Count);
            FormattedText insertedText = paragraph.Elements[0] as FormattedText;
            Assert.NotNull(insertedText);

            // Ensure that the inserted raw text matches and is italic.
            Assert.AreEqual(rawText, ((Text)insertedText.Elements[0]).Content);
        }
    }
}
