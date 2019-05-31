using FluentAssertions;
using System.IO;
using Wps.Client.Models;
using Wps.Client.Models.Data;
using Wps.Client.Models.Responses;
using Wps.Client.Services;
using Xunit;

namespace Wps.Client.Tests
{
    public class ModelDeserializationTests : IClassFixture<XmlSerializationService>
    {

        private readonly IXmlSerializer _serializer;

        public ModelDeserializationTests(XmlSerializationService serializer)
        {
            _serializer = serializer;
        }

        [Fact]
        public void DeserializeFormat_ValidFormatGiven_ShouldPass()
        {
            const string xml = @"<ns:Format xmlns:ns=""http://www.opengis.net/wps/2.0"" mimeType=""application/x-geotiff"" encoding=""base64"" schema=""test.schema.tld"" maximumMegabytes=""2048"" default=""true""/>";

            var format = _serializer.Deserialize<Format>(xml);
            format.Should().NotBeNull();
            if (format != null)
            {
                format.MimeType.Should().Be("application/x-geotiff");
                format.Encoding.Should().Be("base64");
                format.Schema.Should().Be("test.schema.tld");
                format.MaximumMegabytes.Should().Be(2048);
                format.IsDefault.Should().BeTrue();
            }
        }

        [Fact]
        public void DeserializeDataType_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<ows:DataType xmlns:ows=""http://www.opengis.net/ows/2.0"" ows:reference=""string""/>";

            var dataType = _serializer.Deserialize<DataType>(xml);
            dataType.Should().NotBeNull();
            dataType?.Reference.Should().Be("string");
            dataType?.GetReferenceType().Should().Be(typeof(string));
        }

        [Fact]
        public void DeserializeLiteralDataDomain_ValidXmlGiven_WithAllowedValues_ShouldPass()
        {
            const string xml = @"<wps:LiteralDataDomain default=""true"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                        <ows:AllowedValues>
                            <ows:Value>uncorrected</ows:Value>
                            <ows:Value>dos1</ows:Value>
                            <ows:Value>dos2</ows:Value>
                            <ows:Value>dos2b</ows:Value>
                            <ows:Value>dos3</ows:Value>
                            <ows:Value>dos4</ows:Value>
                        </ows:AllowedValues>
                        <ows:DataType ows:reference=""string""/>
                        <ows:DefaultValue>uncorrected</ows:DefaultValue>
                        <ows:ValuesUnit>Meter</ows:ValuesUnit>
                    </wps:LiteralDataDomain>";

            var domain = _serializer.Deserialize<LiteralDataDomain>(xml);
            domain.Should().NotBeNull();
            domain?.IsDefault.Should().BeTrue();
            domain?.DefaultValue.Should().Be("uncorrected");
            domain?.PossibleLiteralValues.GetType().Should().Be(typeof(AllowedValues));
            domain?.DataType?.Reference.Should().Be("string");
            domain?.UnitOfMeasure.Should().Be("Meter");
            if (domain?.PossibleLiteralValues is AllowedValues values)
            {
                values.Values.Length.Should().Be(6);
            }
        }

        [Fact]
        public void DeserializeLiteralDataDomain_ValidXmlGiven_WithAnyValue_ShouldPass()
        {
            const string xml = @"<wps:LiteralDataDomain default=""true"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                        <ows:AnyValue></ows:AnyValue>
                        <ows:DataType ows:reference=""string""/>
                        <ows:DefaultValue>uncorrected</ows:DefaultValue>
                        <ows:ValuesUnit>Meter</ows:ValuesUnit>
                    </wps:LiteralDataDomain>";

            using (var reader = new StringReader(xml))
            {
                var domain = _serializer.Deserialize<LiteralDataDomain>(xml);
                domain.Should().NotBeNull();
                domain?.IsDefault.Should().BeTrue();
                domain?.DefaultValue.Should().Be("uncorrected");
                domain?.PossibleLiteralValues.GetType().Should().Be(typeof(AnyValue));
                domain?.DataType?.Reference.Should().Be("string");
                domain?.UnitOfMeasure.Should().Be("Meter");
            }
        }

        [Fact]
        public void DeserializeLiteralDataDomain_ValidXmlGiven_WithValueReference_ShouldPass()
        {
            const string xml = @"<wps:LiteralDataDomain default=""true"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"">
                        <ows:ValueReference></ows:ValueReference>
                        <ows:DataType ows:reference=""string""/>
                        <ows:DefaultValue>uncorrected</ows:DefaultValue>
                        <ows:ValuesUnit>Meter</ows:ValuesUnit>
                    </wps:LiteralDataDomain>";

            var domain = _serializer.Deserialize<LiteralDataDomain>(xml);
            domain.Should().NotBeNull();
            domain?.IsDefault.Should().BeTrue();
            domain?.DefaultValue.Should().Be("uncorrected");
            domain?.PossibleLiteralValues.GetType().Should().Be(typeof(ValueReference));
            domain?.DataType?.Reference.Should().Be("string");
        }

        [Fact]
        public void DeserializeLiteralData_ValidXmlGiven_WithLiteralDataDomain_ShouldPass()
        {
            const string xml = @"<wps:LiteralData xmlns:ns=""http://www.opengis.net/wps/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <ns:Format default=""true"" mimeType=""text/plain""/>
                            <ns:Format mimeType=""text/xml""/>
                            <LiteralDataDomain>
                                <ows:AnyValue/>
                                <ows:DataType ows:reference=""float""/>
                            </LiteralDataDomain>
                        </wps:LiteralData>";

            using (var reader = new StringReader(xml))
            {
                var data = _serializer.Deserialize<LiteralData>(xml);
                data.Should().NotBeNull();
                data?.Formats.Length.Should().Be(2);
                data?.LiteralDataDomains.Length.Should().Be(1);
            }
        }

        [Fact]
        public void DeserializeOutput_ValidXmlGiven_WithNoNesting_ShouldPass()
        {
            const string xml = @"<wps:Output xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <ows:Title>Module output on stdout</ows:Title>
                            <ows:Abstract>The output of the module written to stdout</ows:Abstract>
                            <ows:Identifier>stdout</ows:Identifier>
                            <wps:ComplexData xmlns:ns=""http://www.opengis.net/wps/2.0"">
                                <ns:Format default=""true"" mimeType=""text/plain""/>
                                <ns:Format mimeType=""text/plain"" encoding=""base64""/>
                            </wps:ComplexData>
                        </wps:Output>";

            var output = _serializer.Deserialize<Output>(xml);
            output?.Data.GetType().Should().Be(typeof(ComplexData));
            output?.Title.Should().Be("Module output on stdout");
            output?.Abstract.Should().Be("The output of the module written to stdout");
            output?.Identifier.Should().Be("stdout");
            output?.Outputs.Should().BeNull();
        }

        [Fact]
        public void DeserializeOutput_ValidXmlGiven_WithOneLevelNesting_ShouldPass()
        {
            const string xml = @"<wps:Output xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <wps:Output>
                            </wps:Output>
                        </wps:Output>";

            var output = _serializer.Deserialize<Output>(xml);
            output?.Outputs.Length.Should().Be(1);
            output?.Outputs[0].GetType().Should().Be(typeof(Output));
        }

        [Fact]
        public void DeserializeInput_ValidXmlGiven_WithNoNesting_ShouldPass()
        {
            const string xml = @"<wps:Input minOccurs=""0"" maxOccurs=""1"" xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <ows:Title>Default: center only</ows:Title>
                            <ows:Identifier>-n</ows:Identifier>
                            <ows:Abstract>Test abstract</ows:Abstract>
                            <wps:LiteralData
                                xmlns:ns=""http://www.opengis.net/wps/2.0"">
                                <ns:Format default=""true"" mimeType=""text/plain""/>
                                <ns:Format mimeType=""text/xml""/>
                                <LiteralDataDomain>
                                    <ows:AllowedValues>
                                        <ows:Value>true</ows:Value>
                                        <ows:Value>false</ows:Value>
                                    </ows:AllowedValues>
                                    <ows:DataType ows:reference=""boolean""/>
                                    <ows:DefaultValue>false</ows:DefaultValue>
                                </LiteralDataDomain>
                            </wps:LiteralData>
                        </wps:Input>";

            var input = _serializer.Deserialize<Input>(xml);
            input?.Data.GetType().Should().Be(typeof(LiteralData));
            input?.MinimumOccurrences.Should().Be(0);
            input?.MaximumOccurrences.Should().Be(1);
            input?.Title.Should().Be("Default: center only");
            input?.Abstract.Should().Be("Test abstract");
            input?.Identifier.Should().Be("-n");
            input?.Inputs.Should().BeNull();
        }

        [Fact]
        public void DeserializeInput_ValidXmlGiven_WithOneLevelNesting_ShouldPass()
        {
            const string xml = @"<wps:Input minOccurs=""0"" maxOccurs=""1"" xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <wps:Input>
                            </wps:Input>
                        </wps:Input>";

            var input = _serializer.Deserialize<Input>(xml);
            input?.Inputs.Length.Should().Be(1);
            input?.Inputs[0].GetType().Should().Be(typeof(Input));
        }

        [Fact]
        public void DeserializeProcess_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<wps:Process xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <ows:Title>Targets an imagery group to a GRASS location and mapset.</ows:Title>
                            <ows:Abstract>http://grass.osgeo.org/grass70/manuals/html70_user/i.target.html</ows:Abstract>
                            <ows:Identifier>i.target</ows:Identifier>
                            <wps:Input minOccurs=""1"" maxOccurs=""1"">
                            </wps:Input>
                            <wps:Input minOccurs=""0"" maxOccurs=""1"">
                            </wps:Input>
                            <wps:Input minOccurs=""0"" maxOccurs=""1"">
                            </wps:Input>
                            <wps:Input minOccurs=""0"" maxOccurs=""1"">
                            </wps:Input>
                            <wps:Output>
                            </wps:Output>
                        </wps:Process>";

            var process = _serializer.Deserialize<Process>(xml);
            process?.Inputs.Length.Should().Be(4);
            process?.Outputs.Length.Should().Be(1);
            process?.Title.Should().Be("Targets an imagery group to a GRASS location and mapset.");
            process?.Abstract.Should().Be("http://grass.osgeo.org/grass70/manuals/html70_user/i.target.html");
            process?.Identifier.Should().Be("i.target");
        }

        [Fact]
        public void DeserializeProcessOffering_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<wps:ProcessOffering processVersion=""1.0.0"" jobControlOptions=""sync-execute async-execute"" outputTransmission=""value reference"" processModel=""test""
                                                xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <wps:Process>
	                        </wps:Process>
                        </wps:ProcessOffering>";

            var processOffering = _serializer.Deserialize<ProcessOffering>(xml);
            processOffering?.ProcessVersion.Should().Be("1.0.0");
            processOffering?.JobControlOptions.Should().Be("sync-execute async-execute");
            processOffering?.OutputTransmission.Should().Be("value reference");
            processOffering?.ProcessModel.Should().Be("test");
            processOffering?.Process.Should().NotBeNull();
            processOffering?.Process.GetType().Should().Be(typeof(Process));
        }

        [Fact]
        public void DeserializeProcessOfferingCollection_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                        <wps:ProcessOfferings xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:xml=""http://www.w3.org/XML/1998/namespace"" xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"" xsi:schemaLocation=""http://www.opengis.net/wps/2.0 http://schemas.opengis.net/wps/2.0/wps.xsd"">
                            <wps:ProcessOffering></wps:ProcessOffering>
                            <wps:ProcessOffering></wps:ProcessOffering>
                            <wps:ProcessOffering></wps:ProcessOffering>
                        </wps:ProcessOfferings>";

            var processOfferingCollection = _serializer.Deserialize<ProcessOfferingCollection>(xml);
            processOfferingCollection?.Count.Should().Be(3);
            if (processOfferingCollection != null)
            {
                foreach (var offering in processOfferingCollection)
                {
                    offering.Should().NotBeNull();
                    offering.GetType().Should().Be(typeof(ProcessOffering));
                }
            }
        }

        [Fact]
        public void DeserializeProcessSummary_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<wps:ProcessSummary processVersion=""1.0.0"" jobControlOptions=""sync-execute async-execute"" outputTransmission=""value reference"" processModel=""test"" xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:xlin=""http://www.w3.org/1999/xlink"" >
                                    <ows:Title>Canonical components analysis (CCA) program for image processing.</ows:Title>
                                    <ows:Identifier>i.cca</ows:Identifier>
                                    <ows:Abstract>Test abstract</ows:Abstract>
                                    <ows:Keywords>
                                        <ows:Keyword>WPS</ows:Keyword>
                                        <ows:Keyword>geospatial</ows:Keyword>
                                        <ows:Keyword>geoprocessing</ows:Keyword>
                                    </ows:Keywords>
                                    <ows:Metadata xlin:role=""Process description"" xlin:href=""http://geoprocessing.demo.52north.org:80/wps/WebProcessingService?service=WPS&amp;request=DescribeProcess&amp;version=2.0.0&amp;identifier=i.cca""/>
                                </wps:ProcessSummary>";

            var summary = _serializer.Deserialize<ProcessSummary>(xml);
            summary.Title.Should().Be("Canonical components analysis (CCA) program for image processing.");
            summary.Identifier.Should().Be("i.cca");
            summary.Abstract.Should().Be("Test abstract");
            summary.ProcessVersion.Should().Be("1.0.0");
            summary.ProcessModel.Should().Be("test");
            summary.JobControlOptions.Should().Be("sync-execute async-execute");
            summary.OutputTransmission.Should().Be("value reference");
            summary.Keywords.Should().BeEquivalentTo("WPS", "geospatial", "geoprocessing");
        }

        [Fact]
        public void DeserializeServiceIdentification_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<ows:ServiceIdentification xmlns:ows=""http://www.opengis.net/ows/2.0"">
                                    <ows:Title>52°North WPS 4.0.0-beta.4-SNAPSHOT</ows:Title>
                                    <ows:Abstract>Service based on the 52°North implementation of WPS 1.0.0 and 2.0.0</ows:Abstract>
                                    <ows:Keywords>
                                        <ows:Keyword>WPS</ows:Keyword>
                                        <ows:Keyword>geospatial</ows:Keyword>
                                        <ows:Keyword>geoprocessing</ows:Keyword>
                                    </ows:Keywords>
                                    <ows:ServiceType>WPS</ows:ServiceType>
                                    <ows:ServiceTypeVersion>1.0.0</ows:ServiceTypeVersion>
                                    <ows:ServiceTypeVersion>2.0.0</ows:ServiceTypeVersion>
                                    <ows:Fees>NONE</ows:Fees>
                                    <ows:AccessConstraints>NONE</ows:AccessConstraints>
                                </ows:ServiceIdentification>";

            var serviceIdentification = _serializer.Deserialize<ServiceIdentification>(xml);
            serviceIdentification.Title.Should().Be("52°North WPS 4.0.0-beta.4-SNAPSHOT");
            serviceIdentification.Abstract.Should()
                .Be("Service based on the 52°North implementation of WPS 1.0.0 and 2.0.0");
            serviceIdentification.Keywords.Should().BeEquivalentTo("WPS", "geospatial", "geoprocessing");
            serviceIdentification.Type.Should().Be("WPS");
            serviceIdentification.Versions.Should().BeEquivalentTo("1.0.0", "2.0.0");
            serviceIdentification.Fees.Should().Be("NONE");
            serviceIdentification.AccessConstraints.Should().Be("NONE");
        }

        [Fact]
        public void DeserializeServiceProvider_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<ows:ServiceProvider xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xlin=""http://www.w3.org/1999/xlink"">
                                  <ows:ProviderName>CompanyName</ows:ProviderName>
                                  <ows:ProviderSite xlin:href=""http://sd.test.tdl""/>
                                  <ows:ServiceContact>
                                  </ows:ServiceContact>
                                </ows:ServiceProvider>";

            var serviceProvider = _serializer.Deserialize<ServiceProvider>(xml);
            serviceProvider.ProviderName.Should().Be("CompanyName");
            serviceProvider.ProviderSite.HyperlinkReference.Should().Be("http://sd.test.tdl");
            serviceProvider.ServiceContact.Should().NotBeNull();
        }

        [Fact]
        public void DeserializeServiceContact_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<ows:ServiceContact xmlns:ows=""http://www.opengis.net/ows/2.0"">
                                    <ows:IndividualName>Test name</ows:IndividualName>
                                    <ows:PositionName>Test position</ows:PositionName>
                                    <ows:ContactInfo/>
                                    <ows:Role>Test role</ows:Role>
                                </ows:ServiceContact>";

            var serviceContact = _serializer.Deserialize<ServiceContact>(xml);
            serviceContact.IndividualName.Should().Be("Test name");
            serviceContact.PositionName.Should().Be("Test position");
            serviceContact.ContactInfo.Should().NotBeNull();
            serviceContact.Role.Should().Be("Test role");
        }

        [Fact]
        public void DeserializeContactInfo_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<ows:ContactInfo xmlns:ows=""http://www.opengis.net/ows/2.0"">
                                    <ows:Phone>
                                        <ows:Voice>test1</ows:Voice>
                                        <ows:Voice>test2</ows:Voice>
                                    </ows:Phone>
                                    <ows:Address/>
                                    <ows:HoursOfService>10am-8pm</ows:HoursOfService>
                                    <ows:ContactInstructions>None</ows:ContactInstructions>
                                </ows:ContactInfo>";
            var contactInfo = _serializer.Deserialize<ContactInfo>(xml);
            contactInfo.Address.Should().NotBeNull();
            contactInfo.ContactInstructions.Should().Be("None");
            contactInfo.Phone.Should().BeEquivalentTo("test1", "test2");
            contactInfo.HoursOfService.Should().Be("10am-8pm");
        }

        [Fact]
        public void DeserializeAddress_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<ows:Address xmlns:ows=""http://www.opengis.net/ows/2.0"">
                                    <ows:DeliveryPoint>Point 1</ows:DeliveryPoint>
                                    <ows:DeliveryPoint>Point 2</ows:DeliveryPoint>
                                    <ows:City>Mars</ows:City>
                                    <ows:AdministrativeArea>None</ows:AdministrativeArea>
                                    <ows:PostalCode>NoZip</ows:PostalCode>
                                    <ows:Country>No country</ows:Country>
                                    <ows:ElectronicMailAddress>test1@example.com</ows:ElectronicMailAddress>
                                    <ows:ElectronicMailAddress>test2@example.com</ows:ElectronicMailAddress>
                                    <ows:ElectronicMailAddress>test3@example.com</ows:ElectronicMailAddress>
                                </ows:Address>";

            var address = _serializer.Deserialize<Address>(xml);
            address.AdministrativeArea.Should().Be("None");
            address.City.Should().Be("Mars");
            address.DeliveryPoints.Should().BeEquivalentTo("Point 1", "Point 2");
            address.Emails.Should().BeEquivalentTo("test1@example.com", "test2@example.com", "test3@example.com");
            address.PostalCode.Should().Be("NoZip");
            address.Country.Should().Be("No country");
        }

        [Fact]
        public void DeserializeOperationParameter_ValidXmlGiven_ShouldPass()
        {
            var xml = @"<ows:Parameter xmlns:ows=""http://www.opengis.net/ows/2.0"" name=""test parameter"">
                            <ows:Value>Value 1</ows:Value>
                            <ows:Value>Value 2</ows:Value>
                        </ows:Parameter>";

            var operationParameter = _serializer.Deserialize<OperationParameter>(xml);
            operationParameter.Name.Should().Be("test parameter");
            operationParameter.Values.Should().BeEquivalentTo("Value 1", "Value 2");
        }

        [Fact]
        public void DeserializeOperationConstraint_ValidXmlGiven_ShouldPass()
        {
            var xml = @"<ows:Constraint xmlns:ows=""http://www.opengis.net/ows/2.0"" name=""test constraint"">
                            <ows:Value>Value 1</ows:Value>
                            <ows:Value>Value 2</ows:Value>
                        </ows:Constraint>";

            var operationParameter = _serializer.Deserialize<OperationConstraint>(xml);
            operationParameter.Name.Should().Be("test constraint");
            operationParameter.Values.Should().BeEquivalentTo("Value 1", "Value 2");
        }

        [Fact]
        public void DeserializeOperationsMetadata_ValidXmlGiven_ShouldPass()
        {
            var xml = @"<ows:OperationsMetadata xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <ows:Operation></ows:Operation>
                            <ows:Operation></ows:Operation>
                            <ows:Operation></ows:Operation>
                            <ows:Operation></ows:Operation>
                            <ows:Constraint></ows:Constraint>
                            <ows:Constraint></ows:Constraint>
                            <ows:Parameter></ows:Parameter>
                            <ows:Parameter></ows:Parameter>
                            <ows:Parameter></ows:Parameter>
                        </ows:OperationsMetadata>";

            var opMetadata = _serializer.Deserialize<OperationsMetadata>(xml);
            opMetadata.Constraints.Length.Should().Be(2);
            opMetadata.Operations.Length.Should().Be(4);
            opMetadata.Parameters.Length.Should().Be(3);
        }

        [Fact]
        public void DeserializeOperation_ValidXmlGiven_ShouldPass()
        {
             const string xml = @"<ows:Operation xmlns:ows=""http://www.opengis.net/ows/2.0"" name=""test operation"">
                            <ows:DCP></ows:DCP>
                            <ows:DCP></ows:DCP>
                            <ows:DCP></ows:DCP>
                            <ows:DCP></ows:DCP>
                            <ows:Constraint></ows:Constraint>
                            <ows:Constraint></ows:Constraint>
                            <ows:Parameter></ows:Parameter>
                            <ows:Parameter></ows:Parameter>
                            <ows:Parameter></ows:Parameter>
                        </ows:Operation>";

            var operation = _serializer.Deserialize<Operation>(xml);
            operation.Name.Should().Be("test operation");
            operation.DistributedComputingPlatforms.Length.Should().Be(4);
            operation.Constraints.Length.Should().Be(2);
            operation.Parameters.Length.Should().Be(3);
        }

        [Fact]
        public void DeserializeDistributedComputingPlatform_ValidXmlGiven_ShouldPass()
        {
            var xml = @"<ows:DCP xmlns:ows=""http://www.opengis.net/ows/2.0"">
                            <ows:HTTP></ows:HTTP>
                        </ows:DCP>";

            var dcp = _serializer.Deserialize<DistributedComputingPlatform>(xml);
            dcp.HttpConnectionPoint.Should().NotBeNull();
        }

        [Fact]
        public void DeserializePlatformConnectionPoint_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<ows:HTTP xmlns:ows=""http://www.opengis.net/ows/2.0"" xmlns:xlin=""http://www.w3.org/1999/xlink"">
                                    <ows:Get xlin:href=""test hyperlink get""/>
                                    <ows:Post xlin:href=""test hyperlink post""/>
                                 </ows:HTTP>";

            var pcp = _serializer.Deserialize<PlatformConnectionPoint>(xml);
            pcp.Get.Hyperlink.Should().Be("test hyperlink get");
            pcp.Post.Hyperlink.Should().Be("test hyperlink post");
        }

        [Fact]
        public void DeserializeGetCapabilitiesResponse_ValidXmlGiven_ShouldPass()
        {
            const string xml = @"<wps:Capabilities xmlns:wps=""http://www.opengis.net/wps/2.0"" xmlns:ows=""http://www.opengis.net/ows/2.0"" version=""2.0.0"" service=""WPS"">
                                    <ows:ServiceIdentification></ows:ServiceIdentification>
                                    <ows:ServiceProvider></ows:ServiceProvider>
                                    <ows:Languages>
                                        <ows:Language>test</ows:Language>
                                    </ows:Languages>
                                    <ows:OperationsMetadata>
                                        <ows:Operation></ows:Operation>
                                    </ows:OperationsMetadata>
                                    <wps:Contents>
                                        <wps:ProcessSummary></wps:ProcessSummary>
                                        <wps:ProcessSummary></wps:ProcessSummary>
                                    </wps:Contents>
                                 </wps:Capabilities>";

            var response = _serializer.Deserialize<GetCapabilitiesResponse>(xml);
            response.ServiceIdentification.Should().NotBeNull();
            response.ServiceProvider.Should().NotBeNull();
            response.OperationsMetadata.Should().NotBeNull();
            response.Languages.Should().NotBeEmpty();
            response.Service.Should().Be("WPS");
            response.Version.Should().Be("2.0.0");
            response.ProcessSummaries.Should().HaveCount(2);
        }

    }
}
