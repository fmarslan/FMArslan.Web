using NLog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace FMArslan.Web.Helper
{

    [XmlRoot("attribute")]
    public class PageAttribute
    {
        [XmlAttribute("key")]
        public String key { get; set; }
        [XmlAttribute("value")]
        public String value { get; set; }
    }

    [XmlRoot("page")]
    public class PageModel
    {
        [XmlIgnore]
        public String this[String key]
        {
            get
            {
                if (Attributes == null)
                    return "";
                PageAttribute value = Attributes.FirstOrDefault(x => x.key != null && x.key.Trim().Equals(key));
                if (value == null)
                    return "";
                else
                    return value.value;
            }
        }

        [XmlElement("title")]
        public String Title { get; set; }
        [XmlElement("changefreg")]
        public String ChangeFreq { get; set; }
        [XmlElement("priority")]
        public String Priority { get; set; }
        [XmlElement(elementName: "fullpage")]
        public Boolean FullPage { get; set; }   
        [XmlElement(elementName:"path", IsNullable = false)]
        public String FilePath { get; set; }
        [XmlElement(elementName: "key", IsNullable = false)]
        public String Key { get; set; }
        [XmlArray(elementName:"attributes", IsNullable = true, Namespace = "")]
        [XmlArrayItem(ElementName ="attribute",Namespace ="")]
        public List<PageAttribute> Attributes { get; set; }

    }

    [XmlRoot("navigation")]
    public class NavigationModel
    {
        [XmlArray(elementName: "pages",IsNullable = false, Namespace = "")]
        [XmlArrayItem(ElementName = "page",IsNullable =false, Namespace = "")]
        public List<PageModel> Pages { get; set; }
    }

    public class NavigationHelper
    {

        public static NavigationModel Navigation { get; private set; }
        static Logger logger = LogManager.GetCurrentClassLogger();

        public static void Init()
        {

            logger.Info("Navigation Initializing...");
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(NavigationModel));
            String configFilePath = HttpContext.Current.Server.MapPath("~/App_Data/navigation.xml");
            if (File.Exists(configFilePath) == false)
            {
                logger.Info("navigation.xml file is writing...");
                #region Sample Config
                Navigation = new NavigationModel();
                Navigation.Pages = new List<PageModel>();
                PageModel mainPage = new PageModel
                {
                    FilePath = "main.cshtml",
                    ChangeFreq = "weekly",
                    Key = "main",
                    Priority = "0.3",
                    Title = "Main Page",
                };
                Navigation.Pages.Add(mainPage);
                PageModel demoPage1 = new PageModel
                {
                    FilePath = "page1.cshtml",
                    ChangeFreq = "weekly",
                    Key = "custom-page-1",
                    Priority = "0.3",
                    Title = "Custom Page 1",
                    Attributes = new List<PageAttribute>()
                };
                demoPage1.Attributes.Add(new PageAttribute
                {
                    key = "trTitle",
                    value = "Sayfa 1"
                });
                demoPage1.Attributes.Add(new PageAttribute
                {
                    key = "enTitle",
                    value = "Page 1"
                });
                Navigation.Pages.Add(demoPage1);

                PageModel demoPage2 = new PageModel
                {
                    FilePath = "page1.cshtml",
                    ChangeFreq = "weekly",
                    Key = "custom-page-2",
                    Priority = "0.8",
                    Title = "Custom Page 2",
                    Attributes = new List<PageAttribute>()
                };
                demoPage2.Attributes.Add(new PageAttribute
                {
                    key = "trTitle",
                    value = "Sayfa 1"
                });
                demoPage2.Attributes.Add(new PageAttribute
                {
                    key = "enTitle",
                    value = "Page 1"
                });
                Navigation.Pages.Add(demoPage2);

                PageModel fullpage = new PageModel
                {
                    FilePath = "fullpage.cshtml",
                    ChangeFreq = "weekly",
                    Key = "full-page-template",
                    Priority = "0.8",
                    Title = "Full Page",
                    Attributes = new List<PageAttribute>()
                };
                fullpage.Attributes.Add(new PageAttribute
                {
                    key = "trTitle",
                    value = "Tam Sayfa"
                });
                fullpage.Attributes.Add(new PageAttribute
                {
                    key = "enTitle",
                    value = "Full Page"
                });
                Navigation.Pages.Add(fullpage);

                XmlWriterSettings writerSettings = new XmlWriterSettings();
                writerSettings.Encoding = Encoding.UTF8;
                writerSettings.Indent = true;
                writerSettings.WriteEndDocumentOnClose = true;
                using (XmlWriter writer = XmlWriter.Create(configFilePath, writerSettings))
                {
                    XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                    ns.Add("", "");
                    xmlSerializer.Serialize(writer, Navigation, ns);
                }

                #endregion
            }
            logger.Info("navigation.xml file is reading..");
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.IgnoreComments = true;
            using (XmlReader reader = XmlReader.Create(configFilePath, settings))
            {
                Navigation = (NavigationModel)xmlSerializer.Deserialize(reader);
            }
            logger.Info(String.Format("Navigation initialized. [{0} Page(s)]", Navigation.Pages.Count));
        }
    }
}