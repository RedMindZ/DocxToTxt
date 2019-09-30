using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DocxToTxt.StyleUtils
{
    public static class StyleHelper
    {
        public static DocDefaults CreateDocDefaults()
        {
            return new DocDefaults()
            {
                ParagraphPropertiesDefault = new ParagraphPropertiesDefault()
                {
                    ParagraphPropertiesBaseStyle = new ParagraphPropertiesBaseStyle()
                    {
                        AutoSpaceDE = new AutoSpaceDE() { Val = true },
                        AutoSpaceDN = new AutoSpaceDN() { Val = true },
                        BiDi = new BiDi() { Val = false },
                        AdjustRightIndent = new AdjustRightIndent() { Val = true },
                        SnapToGrid = null,
                        SpacingBetweenLines = null,
                        ContextualSpacing = null,
                        TopLinePunctuation = null,
                        MirrorIndents = null,
                        SuppressOverlap = null,
                        Justification = null,
                        TextDirection = null,
                        TextAlignment = null,
                        Indentation = null,
                        OverflowPunctuation = null,
                        Kinsoku = null,
                        TextBoxTightWrap = null,
                        SuppressAutoHyphens = null,
                        Tabs = null,
                        Shading = null,
                        ParagraphBorders = null,
                        SuppressLineNumbers = null,
                        NumberingProperties = null,
                        WidowControl = null,
                        FrameProperties = null,
                        PageBreakBefore = null,
                        KeepLines = null,
                        KeepNext = null,
                        WordWrap = null,
                        OutlineLevel = null
                    }
                },

                RunPropertiesDefault = new RunPropertiesDefault()
                {
                    RunPropertiesBaseStyle = new RunPropertiesBaseStyle()
                    {
                        Color = null,
                        Spacing = null,
                        CharacterScale = null,
                        Kern = null,
                        Position = null,
                        FontSize = null,
                        FontSizeComplexScript = null,
                        TextEffect = null,
                        WebHidden = null,
                        Border = null,
                        Shading = null,
                        FitText = null,
                        VerticalTextAlignment = null,
                        Emphasis = null,
                        Languages = null,
                        Underline = null,
                        Vanish = null,
                        NoProof = null,
                        EastAsianLayout = null,
                        RunFonts = null,
                        Bold = null,
                        BoldComplexScript = null,
                        Italic = null,
                        ItalicComplexScript = null,
                        SnapToGrid = null,
                        Caps = null,
                        Strike = null,
                        DoubleStrike = null,
                        Outline = null,
                        Shadow = null,
                        Emboss = null,
                        Imprint = null,
                        SmallCaps = null,
                        SpecVanish = null,
                    }
                }
            };
        }
        private static void CopyStyleProperties(Style source, Style destination)
        {
            #region Style Properties
            destination.Rsid = (Rsid)source.Rsid?.Clone() ?? destination.Rsid;
            destination.PersonalReply = (PersonalReply)source.PersonalReply?.Clone() ?? destination.PersonalReply;
            destination.PersonalCompose = (PersonalCompose)source.PersonalCompose?.Clone() ?? destination.PersonalCompose;
            destination.Personal = (Personal)source.Personal?.Clone() ?? destination.Personal;
            destination.Locked = (Locked)source.Locked?.Clone() ?? destination.Locked;
            destination.PrimaryStyle = (PrimaryStyle)source.PrimaryStyle?.Clone() ?? destination.PrimaryStyle;
            destination.UnhideWhenUsed = (UnhideWhenUsed)source.UnhideWhenUsed?.Clone() ?? destination.UnhideWhenUsed;
            destination.SemiHidden = (SemiHidden)source.SemiHidden?.Clone() ?? destination.SemiHidden;
            destination.UIPriority = (UIPriority)source.UIPriority?.Clone() ?? destination.UIPriority;
            destination.AutoRedefine = (AutoRedefine)source.AutoRedefine?.Clone() ?? destination.AutoRedefine;
            destination.TableStyleConditionalFormattingTableRowProperties = (TableStyleConditionalFormattingTableRowProperties)source.TableStyleConditionalFormattingTableRowProperties?.Clone() ?? destination.TableStyleConditionalFormattingTableRowProperties;
            destination.LinkedStyle = (LinkedStyle)source.LinkedStyle?.Clone() ?? destination.LinkedStyle;
            destination.NextParagraphStyle = (NextParagraphStyle)source.NextParagraphStyle?.Clone() ?? destination.NextParagraphStyle;
            destination.BasedOn = (BasedOn)source.BasedOn?.Clone() ?? destination.BasedOn;
            destination.Aliases = (Aliases)source.Aliases?.Clone() ?? destination.Aliases;
            destination.StyleName = (StyleName)source.StyleName?.Clone() ?? destination.StyleName;
            destination.CustomStyle = (OnOffValue)source.CustomStyle?.Clone() ?? destination.CustomStyle;
            destination.Default = (OnOffValue)source.Default?.Clone() ?? destination.Default;
            destination.StyleId = (StringValue)source.StyleId?.Clone() ?? destination.StyleId;
            destination.Type = (EnumValue<StyleValues>)source.Type?.Clone() ?? destination.Type;
            destination.StyleHidden = (StyleHidden)source.StyleHidden?.Clone() ?? destination.StyleHidden;
            destination.StyleTableCellProperties = (StyleTableCellProperties)source.StyleTableCellProperties?.Clone() ?? destination.StyleTableCellProperties;
            #endregion

            #region StyleParagraph Properties
            if (source.StyleParagraphProperties != null)
            {
                if (destination.StyleParagraphProperties == null)
                {
                    destination.StyleParagraphProperties = new StyleParagraphProperties();
                }

                destination.StyleParagraphProperties.AutoSpaceDN = (AutoSpaceDN)source.StyleParagraphProperties.AutoSpaceDN?.Clone() ?? destination.StyleParagraphProperties.AutoSpaceDN;
                destination.StyleParagraphProperties.BiDi = (BiDi)source.StyleParagraphProperties.BiDi?.Clone() ?? destination.StyleParagraphProperties.BiDi;
                destination.StyleParagraphProperties.AdjustRightIndent = (AdjustRightIndent)source.StyleParagraphProperties.AdjustRightIndent?.Clone() ?? destination.StyleParagraphProperties.AdjustRightIndent;
                destination.StyleParagraphProperties.SnapToGrid = (SnapToGrid)source.StyleParagraphProperties.SnapToGrid?.Clone() ?? destination.StyleParagraphProperties.SnapToGrid;
                destination.StyleParagraphProperties.SpacingBetweenLines = (SpacingBetweenLines)source.StyleParagraphProperties.SpacingBetweenLines?.Clone() ?? destination.StyleParagraphProperties.SpacingBetweenLines;
                destination.StyleParagraphProperties.Indentation = (Indentation)source.StyleParagraphProperties.Indentation?.Clone() ?? destination.StyleParagraphProperties.Indentation;
                destination.StyleParagraphProperties.MirrorIndents = (MirrorIndents)source.StyleParagraphProperties.MirrorIndents?.Clone() ?? destination.StyleParagraphProperties.MirrorIndents;
                destination.StyleParagraphProperties.AutoSpaceDE = (AutoSpaceDE)source.StyleParagraphProperties.AutoSpaceDE?.Clone() ?? destination.StyleParagraphProperties.AutoSpaceDE;
                destination.StyleParagraphProperties.SuppressOverlap = (SuppressOverlap)source.StyleParagraphProperties.SuppressOverlap?.Clone() ?? destination.StyleParagraphProperties.SuppressOverlap;
                destination.StyleParagraphProperties.Justification = (Justification)source.StyleParagraphProperties.Justification?.Clone() ?? destination.StyleParagraphProperties.Justification;
                destination.StyleParagraphProperties.TextDirection = (TextDirection)source.StyleParagraphProperties.TextDirection?.Clone() ?? destination.StyleParagraphProperties.TextDirection;
                destination.StyleParagraphProperties.TextAlignment = (TextAlignment)source.StyleParagraphProperties.TextAlignment?.Clone() ?? destination.StyleParagraphProperties.TextAlignment;
                destination.StyleParagraphProperties.TextBoxTightWrap = (TextBoxTightWrap)source.StyleParagraphProperties.TextBoxTightWrap?.Clone() ?? destination.StyleParagraphProperties.TextBoxTightWrap;
                destination.StyleParagraphProperties.ContextualSpacing = (ContextualSpacing)source.StyleParagraphProperties.ContextualSpacing?.Clone() ?? destination.StyleParagraphProperties.ContextualSpacing;
                destination.StyleParagraphProperties.TopLinePunctuation = (TopLinePunctuation)source.StyleParagraphProperties.TopLinePunctuation?.Clone() ?? destination.StyleParagraphProperties.TopLinePunctuation;
                destination.StyleParagraphProperties.WordWrap = (WordWrap)source.StyleParagraphProperties.WordWrap?.Clone() ?? destination.StyleParagraphProperties.WordWrap;
                destination.StyleParagraphProperties.OutlineLevel = (OutlineLevel)source.StyleParagraphProperties.OutlineLevel?.Clone() ?? destination.StyleParagraphProperties.OutlineLevel;
                destination.StyleParagraphProperties.KeepNext = (KeepNext)source.StyleParagraphProperties.KeepNext?.Clone() ?? destination.StyleParagraphProperties.KeepNext;
                destination.StyleParagraphProperties.KeepLines = (KeepLines)source.StyleParagraphProperties.KeepLines?.Clone() ?? destination.StyleParagraphProperties.KeepLines;
                destination.StyleParagraphProperties.PageBreakBefore = (PageBreakBefore)source.StyleParagraphProperties.PageBreakBefore?.Clone() ?? destination.StyleParagraphProperties.PageBreakBefore;
                destination.StyleParagraphProperties.FrameProperties = (FrameProperties)source.StyleParagraphProperties.FrameProperties?.Clone() ?? destination.StyleParagraphProperties.FrameProperties;
                destination.StyleParagraphProperties.OverflowPunctuation = (OverflowPunctuation)source.StyleParagraphProperties.OverflowPunctuation?.Clone() ?? destination.StyleParagraphProperties.OverflowPunctuation;
                destination.StyleParagraphProperties.WidowControl = (WidowControl)source.StyleParagraphProperties.WidowControl?.Clone() ?? destination.StyleParagraphProperties.WidowControl;
                destination.StyleParagraphProperties.SuppressLineNumbers = (SuppressLineNumbers)source.StyleParagraphProperties.SuppressLineNumbers?.Clone() ?? destination.StyleParagraphProperties.SuppressLineNumbers;
                destination.StyleParagraphProperties.ParagraphBorders = (ParagraphBorders)source.StyleParagraphProperties.ParagraphBorders?.Clone() ?? destination.StyleParagraphProperties.ParagraphBorders;
                destination.StyleParagraphProperties.Shading = (Shading)source.StyleParagraphProperties.Shading?.Clone() ?? destination.StyleParagraphProperties.Shading;
                destination.StyleParagraphProperties.Tabs = (Tabs)source.StyleParagraphProperties.Tabs?.Clone() ?? destination.StyleParagraphProperties.Tabs;
                destination.StyleParagraphProperties.SuppressAutoHyphens = (SuppressAutoHyphens)source.StyleParagraphProperties.SuppressAutoHyphens?.Clone() ?? destination.StyleParagraphProperties.SuppressAutoHyphens;
                destination.StyleParagraphProperties.Kinsoku = (Kinsoku)source.StyleParagraphProperties.Kinsoku?.Clone() ?? destination.StyleParagraphProperties.Kinsoku;
                destination.StyleParagraphProperties.NumberingProperties = (NumberingProperties)source.StyleParagraphProperties.NumberingProperties?.Clone() ?? destination.StyleParagraphProperties.NumberingProperties;
                destination.StyleParagraphProperties.ParagraphPropertiesChange = (ParagraphPropertiesChange)source.StyleParagraphProperties.ParagraphPropertiesChange?.Clone() ?? destination.StyleParagraphProperties.ParagraphPropertiesChange;
            }
            #endregion

            #region StyleRun Properties
            if (source.StyleRunProperties != null)
            {
                if (destination.StyleRunProperties == null)
                {
                    destination.StyleRunProperties = new StyleRunProperties();
                }

                destination.StyleRunProperties.Spacing = (Spacing)source.StyleRunProperties.Spacing?.Clone() ?? destination.StyleRunProperties.Spacing;
                destination.StyleRunProperties.CharacterScale = (CharacterScale)source.StyleRunProperties.CharacterScale?.Clone() ?? destination.StyleRunProperties.CharacterScale;
                destination.StyleRunProperties.Kern = (Kern)source.StyleRunProperties.Kern?.Clone() ?? destination.StyleRunProperties.Kern;
                destination.StyleRunProperties.Position = (Position)source.StyleRunProperties.Position?.Clone() ?? destination.StyleRunProperties.Position;
                destination.StyleRunProperties.FontSize = (FontSize)source.StyleRunProperties.FontSize?.Clone() ?? destination.StyleRunProperties.FontSize;
                destination.StyleRunProperties.FontSizeComplexScript = (FontSizeComplexScript)source.StyleRunProperties.FontSizeComplexScript?.Clone() ?? destination.StyleRunProperties.FontSizeComplexScript;
                destination.StyleRunProperties.Underline = (Underline)source.StyleRunProperties.Underline?.Clone() ?? destination.StyleRunProperties.Underline;
                destination.StyleRunProperties.Border = (Border)source.StyleRunProperties.Border?.Clone() ?? destination.StyleRunProperties.Border;
                destination.StyleRunProperties.Color = (Color)source.StyleRunProperties.Color?.Clone() ?? destination.StyleRunProperties.Color;
                destination.StyleRunProperties.Shading = (Shading)source.StyleRunProperties.Shading?.Clone() ?? destination.StyleRunProperties.Shading;
                destination.StyleRunProperties.FitText = (FitText)source.StyleRunProperties.FitText?.Clone() ?? destination.StyleRunProperties.FitText;
                destination.StyleRunProperties.VerticalTextAlignment = (VerticalTextAlignment)source.StyleRunProperties.VerticalTextAlignment?.Clone() ?? destination.StyleRunProperties.VerticalTextAlignment;
                destination.StyleRunProperties.Emphasis = (Emphasis)source.StyleRunProperties.Emphasis?.Clone() ?? destination.StyleRunProperties.Emphasis;
                destination.StyleRunProperties.Languages = (Languages)source.StyleRunProperties.Languages?.Clone() ?? destination.StyleRunProperties.Languages;
                destination.StyleRunProperties.EastAsianLayout = (EastAsianLayout)source.StyleRunProperties.EastAsianLayout?.Clone() ?? destination.StyleRunProperties.EastAsianLayout;
                destination.StyleRunProperties.TextEffect = (TextEffect)source.StyleRunProperties.TextEffect?.Clone() ?? destination.StyleRunProperties.TextEffect;
                destination.StyleRunProperties.WebHidden = (WebHidden)source.StyleRunProperties.WebHidden?.Clone() ?? destination.StyleRunProperties.WebHidden;
                destination.StyleRunProperties.SnapToGrid = (SnapToGrid)source.StyleRunProperties.SnapToGrid?.Clone() ?? destination.StyleRunProperties.SnapToGrid;
                destination.StyleRunProperties.SpecVanish = (SpecVanish)source.StyleRunProperties.SpecVanish?.Clone() ?? destination.StyleRunProperties.SpecVanish;
                destination.StyleRunProperties.RunFonts = (RunFonts)source.StyleRunProperties.RunFonts?.Clone() ?? destination.StyleRunProperties.RunFonts;
                destination.StyleRunProperties.Bold = (Bold)source.StyleRunProperties.Bold?.Clone() ?? destination.StyleRunProperties.Bold;
                destination.StyleRunProperties.BoldComplexScript = (BoldComplexScript)source.StyleRunProperties.BoldComplexScript?.Clone() ?? destination.StyleRunProperties.BoldComplexScript;
                destination.StyleRunProperties.Italic = (Italic)source.StyleRunProperties.Italic?.Clone() ?? destination.StyleRunProperties.Italic;
                destination.StyleRunProperties.ItalicComplexScript = (ItalicComplexScript)source.StyleRunProperties.ItalicComplexScript?.Clone() ?? destination.StyleRunProperties.ItalicComplexScript;
                destination.StyleRunProperties.Vanish = (Vanish)source.StyleRunProperties.Vanish?.Clone() ?? destination.StyleRunProperties.Vanish;
                destination.StyleRunProperties.Caps = (Caps)source.StyleRunProperties.Caps?.Clone() ?? destination.StyleRunProperties.Caps;
                destination.StyleRunProperties.Strike = (Strike)source.StyleRunProperties.Strike?.Clone() ?? destination.StyleRunProperties.Strike;
                destination.StyleRunProperties.DoubleStrike = (DoubleStrike)source.StyleRunProperties.DoubleStrike?.Clone() ?? destination.StyleRunProperties.DoubleStrike;
                destination.StyleRunProperties.Outline = (Outline)source.StyleRunProperties.Outline?.Clone() ?? destination.StyleRunProperties.Outline;
                destination.StyleRunProperties.Shadow = (Shadow)source.StyleRunProperties.Shadow?.Clone() ?? destination.StyleRunProperties.Shadow;
                destination.StyleRunProperties.Emboss = (Emboss)source.StyleRunProperties.Emboss?.Clone() ?? destination.StyleRunProperties.Emboss;
                destination.StyleRunProperties.Imprint = (Imprint)source.StyleRunProperties.Imprint?.Clone() ?? destination.StyleRunProperties.Imprint;
                destination.StyleRunProperties.NoProof = (NoProof)source.StyleRunProperties.NoProof?.Clone() ?? destination.StyleRunProperties.NoProof;
                destination.StyleRunProperties.SmallCaps = (SmallCaps)source.StyleRunProperties.SmallCaps?.Clone() ?? destination.StyleRunProperties.SmallCaps;
                destination.StyleRunProperties.RunPropertiesChange = (RunPropertiesChange)source.StyleRunProperties.RunPropertiesChange?.Clone() ?? destination.StyleRunProperties.RunPropertiesChange;
            }
            #endregion

            #region StyleTable Properties
            if (source.StyleTableProperties != null)
            {
                if (destination.StyleTableProperties == null)
                {
                    destination.StyleTableProperties = new StyleTableProperties();
                }

                destination.StyleTableProperties.TableStyleRowBandSize = (TableStyleRowBandSize)source.StyleTableProperties.TableStyleRowBandSize?.Clone() ?? destination.StyleTableProperties.TableStyleRowBandSize;
                destination.StyleTableProperties.TableStyleColumnBandSize = (TableStyleColumnBandSize)source.StyleTableProperties.TableStyleColumnBandSize?.Clone() ?? destination.StyleTableProperties.TableStyleColumnBandSize;
                destination.StyleTableProperties.TableJustification = (TableJustification)source.StyleTableProperties.TableJustification?.Clone() ?? destination.StyleTableProperties.TableJustification;
                destination.StyleTableProperties.TableCellSpacing = (TableCellSpacing)source.StyleTableProperties.TableCellSpacing?.Clone() ?? destination.StyleTableProperties.TableCellSpacing;
                destination.StyleTableProperties.TableIndentation = (TableIndentation)source.StyleTableProperties.TableIndentation?.Clone() ?? destination.StyleTableProperties.TableIndentation;
                destination.StyleTableProperties.TableBorders = (TableBorders)source.StyleTableProperties.TableBorders?.Clone() ?? destination.StyleTableProperties.TableBorders;
                destination.StyleTableProperties.Shading = (Shading)source.StyleTableProperties.Shading?.Clone() ?? destination.StyleTableProperties.Shading;
                destination.StyleTableProperties.TableCellMarginDefault = (TableCellMarginDefault)source.StyleTableProperties.TableCellMarginDefault?.Clone() ?? destination.StyleTableProperties.TableCellMarginDefault;
            }
            #endregion
        }

        public static Style DocDefaultsToStyle(DocDefaults docDefaults)
        {
            return new Style
            {
                StyleParagraphProperties = new StyleParagraphProperties(docDefaults.ParagraphPropertiesDefault.ParagraphPropertiesBaseStyle.OuterXml),
                StyleRunProperties = new StyleRunProperties(docDefaults.RunPropertiesDefault.RunPropertiesBaseStyle.OuterXml),
                StyleId = new DocumentFormat.OpenXml.StringValue("DocxToTxt.DocDefaultsId"),
                StyleName = new StyleName() { Val = "DocxToTxt.DocDefaultsName" }
            };
        }

        public static StyleTree BuildStyleTree(WordprocessingDocument doc)
        {
            StyleTree styleTree = new StyleTree();
            List<StyleNode> styleNodes = new List<StyleNode>();

            Styles docStyles = doc.MainDocumentPart.StyleDefinitionsPart.Styles;

            DocDefaults docDefaults = docStyles.OfType<DocDefaults>().FirstOrDefault() ?? CreateDocDefaults();

            styleTree.Root = new StyleNode(DocDefaultsToStyle(docDefaults));
            styleNodes.AddRange(docStyles.OfType<Style>().Select(x => new StyleNode(x)));

            foreach (StyleNode node in styleNodes)
            {
                if (node.Style.BasedOn != null)
                {
                    node.Parent = styleNodes.Find(x => x.Style.StyleId.Value == node.Style.BasedOn.Val.Value);
                }
                else
                {
                    node.Parent = styleTree.Root;
                }

                node.Parent.Children.Add(node);
            }

            return styleTree;
        }

        private static Style InheritStyle(Style parent, Style child)
        {
            Style inheritedStyle = (Style)parent.Clone();
            CopyStyleProperties(child, inheritedStyle);
            return inheritedStyle;
        }

        public static StyleTree ResolveStyleTreeInheritance(StyleTree styleTree)
        {
            StyleTree resolvedTree = styleTree.Clone();

            resolvedTree.Iterate((x, d) => x.Style = x.Parent != null ? InheritStyle(x.Parent.Style, x.Style) : x.Style);

            return resolvedTree;
        }
    }
}
