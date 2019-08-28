using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UPS_API.Models;
using UPS_API.UPS_Rate;

namespace UPS_API.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
          
           

            UPSSecurity upss = new UPSSecurity();


               UPSSecurityServiceAccessToken upssSvcAccessToken = new UPSSecurityServiceAccessToken();
            upssSvcAccessToken.AccessLicenseNumber = "3D6A1DD5F39023B5";
            upss.ServiceAccessToken = upssSvcAccessToken;
            UPSSecurityUsernameToken upssUsrNameToken = new UPSSecurityUsernameToken();
            upssUsrNameToken.Username = "Developer2019";
            upssUsrNameToken.Password = "Developer=2019";
            upss.UsernameToken = upssUsrNameToken;

            RateRequest rateRequest = new RateRequest();

            RequestType request = new RequestType();
            String[] requestOption = { "Shoptimeintransit" };
            request.RequestOption = requestOption;
            rateRequest.Request = request;


            ShipmentType shipment = new ShipmentType();

            TimeInTransitRequestType Time_Tran = new TimeInTransitRequestType();
            var packbillcode = "03";
            Time_Tran.PackageBillType = packbillcode;
            shipment.DeliveryTimeInformation = Time_Tran;

            ShipperType shipper = new ShipperType();

           // shipper.ShipperNumber = "Your Shipper Number";

            var  shipperAddress = new AddressType();
            // String[] addressLine = { "5555 main", "4 Case Cour", "Apt 3B" };
            //shipperAddress.AddressLine = addressLine;
            shipperAddress.City = "San Diego";
            shipperAddress.PostalCode = "92101";
            shipperAddress.StateProvinceCode = "CA";
            shipperAddress.CountryCode = "US";
            // shipperAddress.AddressLine = addressLine;
            shipper.Address = shipperAddress;
            shipment.Shipper = shipper;
            ShipFromType shipFrom = new ShipFromType();
            var shipFromAddress = new ShipAddressType();
            //shipFromAddress.AddressLine = addressLine;
            shipFromAddress.City = "San Diego";
            shipFromAddress.PostalCode = "92101";
            shipFromAddress.StateProvinceCode = "CA";
            shipFromAddress.CountryCode = "US";
            shipFrom.Address = shipFromAddress;
            shipment.ShipFrom = shipFrom;


            ShipToType shipTo = new ShipToType();
            ShipToAddressType shipToAddress = new ShipToAddressType();
            //String[] addressLine1 = { "10 E. Ritchie Way", "2", "Apt 3B" };
            //shipToAddress.AddressLine = addressLine1;
            shipToAddress.City = "Canton";
            shipToAddress.PostalCode = "02021";
            shipToAddress.StateProvinceCode = "MA";
            shipToAddress.CountryCode = "US";
            shipTo.Address = shipToAddress;
            shipment.ShipTo = shipTo;

            CodeDescriptionType service = new CodeDescriptionType();

            //Below code uses dummy date for reference. Please udpate as required.
            service.Code = "02";
            shipment.Service = service;

            PackageType package = new PackageType();
            PackageWeightType packageWeight = new PackageWeightType();
            packageWeight.Weight = "1";
            CodeDescriptionType uom = new CodeDescriptionType();
            uom.Code = "LBS";
            uom.Description = "pounds";
            packageWeight.UnitOfMeasurement = uom;
            package.PackageWeight = packageWeight;
            CodeDescriptionType packType = new CodeDescriptionType();
            packType.Code = "02";
            package.PackagingType = packType;
            PackageType[] pkgArray = { package };
            shipment.Package = pkgArray;
            rateRequest.Shipment = shipment;
            //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11; //This line will ensure the latest security protocol for consuming the web service call.
            // Console.WriteLine(rateRequest);
            var client = new RatePortTypeClient();
            RateResponse rateResponse = client.ProcessRate(upss,rateRequest);

            var model = new Rate_Package()
            {
                Response = rateResponse
            }; 

            return View(model );
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}