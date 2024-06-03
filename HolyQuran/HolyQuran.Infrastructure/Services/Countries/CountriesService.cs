using AutoMapper;
using HolyQuran.Data.Data;
using Microsoft.EntityFrameworkCore;
using HolyQuran.Core.Dtos;
using HolyQuran.Core.Exceptions;
using HolyQuran.Core.ViewModels;
using HolyQuran.Core.Constants;
using HolyQuran.Infrastructure.Services.Files;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using HolyQuran.Core.General;
using HolyQuran.Core.ViewModel.Paginations;
using HolyQuran.Data.Models;
using DocumentFormat.OpenXml.ExtendedProperties;
using DocumentFormat.OpenXml.InkML;

namespace HolyQuran.Infrastructure.Services.Countries
{
    public class CountriesService : ICountriesService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public CountriesService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
       
        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Countries.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<Country> query = _dbContext.Countries
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<CountryViewModel>>(modelquery);
            var pagingResult = new PagingAPIViewModel
            {
                Data = modelViewModels,
                NumberOfPages = totalPages,
                CureentPage = page
            };

            return pagingResult;
        }
        public async Task CreateDefaultData()
        {
            await _dbContext.Countries.AddRangeAsync(
              new Country() { Code = "AF", NameEn = "Afghanistan", NameAr = "أفغانستان", LanguageEn = "Afghan", LanguageAr = "أفغانستاني" },
              new Country() { Code = "AL", NameEn = "Albania", NameAr = "ألبانيا", LanguageEn = "Albanian", LanguageAr = "ألباني" },
              new Country() { Code = "AX", NameEn = "Aland Islands", NameAr = "جزر آلاند", LanguageEn = "Aland Islander", LanguageAr = "آلاندي" },
              new Country() { Code = "DZ", NameEn = "Algeria", NameAr = "الجزائر", LanguageEn = "Algerian", LanguageAr = "جزائري" },
              new Country() { Code = "AS", NameEn = "American Samoa", NameAr = "ساموا-الأمريكي", LanguageEn = "American Samoan", LanguageAr = "أمريكي سامواني" },
              new Country() { Code = "AD", NameEn = "Andorra", NameAr = "أندورا", LanguageEn = "Andorran", LanguageAr = "أندوري" },
              new Country() { Code = "AO", NameEn = "Angola", NameAr = "أنغولا", LanguageEn = "Angolan", LanguageAr = "أنقولي" },
              new Country() { Code = "AI", NameEn = "Anguilla", NameAr = "أنغويلا", LanguageEn = "Anguillan", LanguageAr = "أنغويلي" },
              new Country() { Code = "AQ", NameEn = "Antarctica", NameAr = "أنتاركتيكا", LanguageEn = "Antarctican", LanguageAr = "أنتاركتيكي" },
              new Country() { Code = "AG", NameEn = "Antigua and Barbuda", NameAr = "أنتيغوا وبربودا", LanguageEn = "Antiguan", LanguageAr = "بربودي" },
              new Country() { Code = "AR", NameEn = "Argentina", NameAr = "الأرجنتين", LanguageEn = "Argentinian", LanguageAr = "أرجنتيني" },
              new Country() { Code = "AM", NameEn = "Armenia", NameAr = "أرمينيا", LanguageEn = "Armenian", LanguageAr = "أرميني" },
              new Country() { Code = "AW", NameEn = "Aruba", NameAr = "أروبه", LanguageEn = "Aruban", LanguageAr = "أوروبهيني" },
              new Country() { Code = "AU", NameEn = "Australia", NameAr = "أستراليا", LanguageEn = "Australian", LanguageAr = "أسترالي" },
              new Country() { Code = "AT", NameEn = "Austria", NameAr = "النمسا", LanguageEn = "Austrian", LanguageAr = "نمساوي" },
              new Country() { Code = "AZ", NameEn = "Azerbaijan", NameAr = "أذربيجان", LanguageEn = "Azerbaijani", LanguageAr = "أذربيجاني" },
              new Country() { Code = "BS", NameEn = "Bahamas", NameAr = "الباهاماس", LanguageEn = "Bahamian", LanguageAr = "باهاميسي" },
              new Country() { Code = "BH", NameEn = "Bahrain", NameAr = "البحرين", LanguageEn = "Bahraini", LanguageAr = "بحريني" },
              new Country() { Code = "BD", NameEn = "Bangladesh", NameAr = "بنغلاديش", LanguageEn = "Bangladeshi", LanguageAr = "بنغلاديشي" },
              new Country() { Code = "BB", NameEn = "Barbados", NameAr = "بربادوس", LanguageEn = "Barbadian", LanguageAr = "بربادوسي" },
              new Country() { Code = "BY", NameEn = "Belarus", NameAr = "روسيا البيضاء", LanguageEn = "Belarusian", LanguageAr = "روسي" },
              new Country() { Code = "BE", NameEn = "Belgium", NameAr = "بلجيكا", LanguageEn = "Belgian", LanguageAr = "بلجيكي" },
              new Country() { Code = "BZ", NameEn = "Belize", NameAr = "بيليز", LanguageEn = "Belizean", LanguageAr = "بيليزي" },
              new Country() { Code = "BJ", NameEn = "Benin", NameAr = "بنين", LanguageEn = "Beninese", LanguageAr = "بنيني" },
              new Country() { Code = "BL", NameEn = "Saint Barthelemy", NameAr = "سان بارتيلمي", LanguageEn = "Saint Barthelmian", LanguageAr = "سان بارتيلمي" },
              new Country() { Code = "BM", NameEn = "Bermuda", NameAr = "جزر برمودا", LanguageEn = "Bermudan", LanguageAr = "برمودي" },
              new Country() { Code = "BT", NameEn = "Bhutan", NameAr = "بوتان", LanguageEn = "Bhutanese", LanguageAr = "بوتاني" },
              new Country() { Code = "BO", NameEn = "Bolivia", NameAr = "بوليفيا", LanguageEn = "Bolivian", LanguageAr = "بوليفي" },
              new Country() { Code = "BA", NameEn = "Bosnia and Herzegovina", NameAr = "البوسنة و الهرسك", LanguageEn = "Bosnian / Herzegovinian", LanguageAr = "بوسني/هرسكي" },
              new Country() { Code = "BW", NameEn = "Botswana", NameAr = "بوتسوانا", LanguageEn = "Botswanan", LanguageAr = "بوتسواني" },
              new Country() { Code = "BV", NameEn = "Bouvet Island", NameAr = "جزيرة بوفيه", LanguageEn = "Bouvetian", LanguageAr = "بوفيهي" },
              new Country() { Code = "BR", NameEn = "Brazil", NameAr = "البرازيل", LanguageEn = "Brazilian", LanguageAr = "برازيلي" },
              new Country() { Code = "IO", NameEn = "British Indian Ocean Territory", NameAr = "إقليم المحيط الهندي البريطاني", LanguageEn = "British Indian Ocean Territory", LanguageAr = "إقليم المحيط الهندي البريطاني" },
              new Country() { Code = "BN", NameEn = "Brunei Darussalam", NameAr = "بروني", LanguageEn = "Bruneian", LanguageAr = "بروني" },
              new Country() { Code = "BG", NameEn = "Bulgaria", NameAr = "بلغاريا", LanguageEn = "Bulgarian", LanguageAr = "بلغاري" },
              new Country() { Code = "BF", NameEn = "Burkina Faso", NameAr = "بوركينا فاسو", LanguageEn = "Burkinabe", LanguageAr = "بوركيني" },
              new Country() { Code = "BI", NameEn = "Burundi", NameAr = "بوروندي", LanguageEn = "Burundian", LanguageAr = "بورونيدي" },
              new Country() { Code = "KH", NameEn = "Cambodia", NameAr = "كمبوديا", LanguageEn = "Cambodian", LanguageAr = "كمبودي" },
              new Country() { Code = "CM", NameEn = "Cameroon", NameAr = "كاميرون", LanguageEn = "Cameroonian", LanguageAr = "كاميروني" },
              new Country() { Code = "CA", NameEn = "Canada", NameAr = "كندا", LanguageEn = "Canadian", LanguageAr = "كندي" },
              new Country() { Code = "CV", NameEn = "Cape Verde", NameAr = "الرأس الأخضر", LanguageEn = "Cape Verdean", LanguageAr = "الرأس الأخضر" },
              new Country() { Code = "KY", NameEn = "Cayman Islands", NameAr = "جزر كايمان", LanguageEn = "Caymanian", LanguageAr = "كايماني" },
              new Country() { Code = "CF", NameEn = "Central African Republic", NameAr = "جمهورية أفريقيا الوسطى", LanguageEn = "Central African", LanguageAr = "أفريقي" },
              new Country() { Code = "TD", NameEn = "Chad", NameAr = "تشاد", LanguageEn = "Chadian", LanguageAr = "تشادي" },
              new Country() { Code = "CL", NameEn = "Chile", NameAr = "شيلي", LanguageEn = "Chilean", LanguageAr = "شيلي" },
              new Country() { Code = "CN", NameEn = "China", NameAr = "الصين", LanguageEn = "Chinese", LanguageAr = "صيني" },
              new Country() { Code = "CX", NameEn = "Christmas Island", NameAr = "جزيرة عيد الميلاد", LanguageEn = "Christmas Islander", LanguageAr = "جزيرة عيد الميلاد" },
              new Country() { Code = "CC", NameEn = "Cocos (Keeling) Islands", NameAr = "جزر كوكوس", LanguageEn = "Cocos Islander", LanguageAr = "جزر كوكوس" },
              new Country() { Code = "CO", NameEn = "Colombia", NameAr = "كولومبيا", LanguageEn = "Colombian", LanguageAr = "كولومبي" },
              new Country() { Code = "KM", NameEn = "Comoros", NameAr = "جزر القمر", LanguageEn = "Comorian", LanguageAr = "جزر القمر" },
              new Country() { Code = "CG", NameEn = "Congo", NameAr = "الكونغو", LanguageEn = "Congolese", LanguageAr = "كونغي" },
              new Country() { Code = "CK", NameEn = "Cook Islands", NameAr = "جزر كوك", LanguageEn = "Cook Islander", LanguageAr = "جزر كوك" },
              new Country() { Code = "CR", NameEn = "Costa Rica", NameAr = "كوستاريكا", LanguageEn = "Costa Rican", LanguageAr = "كوستاريكي" },
              new Country() { Code = "HR", NameEn = "Croatia", NameAr = "كرواتيا", LanguageEn = "Croatian", LanguageAr = "كوراتي" },
              new Country() { Code = "CU", NameEn = "Cuba", NameAr = "كوبا", LanguageEn = "Cuban", LanguageAr = "كوبي" },
              new Country() { Code = "CY", NameEn = "Cyprus", NameAr = "قبرص", LanguageEn = "Cypriot", LanguageAr = "قبرصي" },
              new Country() { Code = "CW", NameEn = "Curaçao", NameAr = "كوراساو", LanguageEn = "Curacian", LanguageAr = "كوراساوي" },
              new Country() { Code = "CZ", NameEn = "Czech Republic", NameAr = "الجمهورية التشيكية", LanguageEn = "Czech", LanguageAr = "تشيكي" },
              new Country() { Code = "DK", NameEn = "Denmark", NameAr = "الدانمارك", LanguageEn = "Danish", LanguageAr = "دنماركي" },
              new Country() { Code = "DJ", NameEn = "Djibouti", NameAr = "جيبوتي", LanguageEn = "Djiboutian", LanguageAr = "جيبوتي" },
              new Country() { Code = "DM", NameEn = "Dominica", NameAr = "دومينيكا", LanguageEn = "Dominican", LanguageAr = "دومينيكي" },
              new Country() { Code = "DO", NameEn = "Dominican Republic", NameAr = "الجمهورية الدومينيكية", LanguageEn = "Dominican", LanguageAr = "دومينيكي" },
              new Country() { Code = "EC", NameEn = "Ecuador", NameAr = "إكوادور", LanguageEn = "Ecuadorian", LanguageAr = "إكوادوري" },
              new Country() { Code = "EG", NameEn = "Egypt", NameAr = "مصر", LanguageEn = "Egyptian", LanguageAr = "مصري" },
              new Country() { Code = "SV", NameEn = "El Salvador", NameAr = "إلسلفادور", LanguageEn = "Salvadoran", LanguageAr = "سلفادوري" },
              new Country() { Code = "GQ", NameEn = "Equatorial Guinea", NameAr = "غينيا الاستوائي", LanguageEn = "Equatorial Guinean", LanguageAr = "غيني" },
              new Country() { Code = "ER", NameEn = "Eritrea", NameAr = "إريتريا", LanguageEn = "Eritrean", LanguageAr = "إريتيري" },
              new Country() { Code = "EE", NameEn = "Estonia", NameAr = "استونيا", LanguageEn = "Estonian", LanguageAr = "استوني" },
              new Country() { Code = "ET", NameEn = "Ethiopia", NameAr = "أثيوبيا", LanguageEn = "Ethiopian", LanguageAr = "أثيوبي" },
              new Country() { Code = "FK", NameEn = "Falkland Islands (Malvinas)", NameAr = "جزر فوكلاند", LanguageEn = "Falkland Islander", LanguageAr = "فوكلاندي" },
              new Country() { Code = "FO", NameEn = "Faroe Islands", NameAr = "جزر فارو", LanguageEn = "Faroese", LanguageAr = "جزر فارو" },
              new Country() { Code = "FJ", NameEn = "Fiji", NameAr = "فيجي", LanguageEn = "Fijian", LanguageAr = "فيجي" },
              new Country() { Code = "FI", NameEn = "Finland", NameAr = "فنلندا", LanguageEn = "Finnish", LanguageAr = "فنلندي" },
              new Country() { Code = "FR", NameEn = "France", NameAr = "فرنسا", LanguageEn = "French", LanguageAr = "فرنسي" },
              new Country() { Code = "GF", NameEn = "French Guiana", NameAr = "غويانا الفرنسية", LanguageEn = "French Guianese", LanguageAr = "غويانا الفرنسية" },
              new Country() { Code = "PF", NameEn = "French Polynesia", NameAr = "بولينيزيا الفرنسية", LanguageEn = "French Polynesian", LanguageAr = "بولينيزيي" },
              new Country() { Code = "TF", NameEn = "French Southern and Antarctic Lands", NameAr = "أراض فرنسية جنوبية وأنتارتيكية", LanguageEn = "French", LanguageAr = "أراض فرنسية جنوبية وأنتارتيكية" },
              new Country() { Code = "GA", NameEn = "Gabon", NameAr = "الغابون", LanguageEn = "Gabonese", LanguageAr = "غابوني" },
              new Country() { Code = "GM", NameEn = "Gambia", NameAr = "غامبيا", LanguageEn = "Gambian", LanguageAr = "غامبي" },
              new Country() { Code = "GE", NameEn = "Georgia", NameAr = "جيورجيا", LanguageEn = "Georgian", LanguageAr = "جيورجي" },
              new Country() { Code = "DE", NameEn = "Germany", NameAr = "ألمانيا", LanguageEn = "German", LanguageAr = "ألماني" },
              new Country() { Code = "GH", NameEn = "Ghana", NameAr = "غانا", LanguageEn = "Ghanaian", LanguageAr = "غاني" },
              new Country() { Code = "GI", NameEn = "Gibraltar", NameAr = "جبل طارق", LanguageEn = "Gibraltar", LanguageAr = "جبل طارق" },
              new Country() { Code = "GG", NameEn = "Guernsey", NameAr = "غيرنزي", LanguageEn = "Guernsian", LanguageAr = "غيرنزي" },
              new Country() { Code = "GR", NameEn = "Greece", NameAr = "اليونان", LanguageEn = "Greek", LanguageAr = "يوناني" },
              new Country() { Code = "GL", NameEn = "Greenland", NameAr = "جرينلاند", LanguageEn = "Greenlandic", LanguageAr = "جرينلاندي" },
              new Country() { Code = "GD", NameEn = "Grenada", NameAr = "غرينادا", LanguageEn = "Grenadian", LanguageAr = "غرينادي" },
              new Country() { Code = "GP", NameEn = "Guadeloupe", NameAr = "جزر جوادلوب", LanguageEn = "Guadeloupe", LanguageAr = "جزر جوادلوب" },
              new Country() { Code = "GU", NameEn = "Guam", NameAr = "جوام", LanguageEn = "Guamanian", LanguageAr = "جوامي" },
              new Country() { Code = "GT", NameEn = "Guatemala", NameAr = "غواتيمال", LanguageEn = "Guatemalan", LanguageAr = "غواتيمالي" },
              new Country() { Code = "GN", NameEn = "Guinea", NameAr = "غينيا", LanguageEn = "Guinean", LanguageAr = "غيني" },
              new Country() { Code = "GW", NameEn = "Guinea-Bissau", NameAr = "غينيا-بيساو", LanguageEn = "Guinea-Bissauan", LanguageAr = "غيني" },
              new Country() { Code = "GY", NameEn = "Guyana", NameAr = "غيانا", LanguageEn = "Guyanese", LanguageAr = "غياني" },
              new Country() { Code = "HT", NameEn = "Haiti", NameAr = "هايتي", LanguageEn = "Haitian", LanguageAr = "هايتي" },
              new Country() { Code = "HM", NameEn = "Heard and Mc Donald Islands", NameAr = "جزيرة هيرد وجزر ماكدونالد", LanguageEn = "Heard and Mc Donald Islanders", LanguageAr = "جزيرة هيرد وجزر ماكدونالد" },
              new Country() { Code = "HN", NameEn = "Honduras", NameAr = "هندوراس", LanguageEn = "Honduran", LanguageAr = "هندوراسي" },
              new Country() { Code = "HK", NameEn = "Hong Kong", NameAr = "هونغ كونغ", LanguageEn = "Hongkongese", LanguageAr = "هونغ كونغي" },
              new Country() { Code = "HU", NameEn = "Hungary", NameAr = "المجر", LanguageEn = "Hungarian", LanguageAr = "مجري" },
              new Country() { Code = "IS", NameEn = "Iceland", NameAr = "آيسلندا", LanguageEn = "Icelandic", LanguageAr = "آيسلندي" },
              new Country() { Code = "IN", NameEn = "India", NameAr = "الهند", LanguageEn = "Indian", LanguageAr = "هندي" },
              new Country() { Code = "IM", NameEn = "Isle of Man", NameAr = "جزيرة مان", LanguageEn = "Manx", LanguageAr = "ماني" },
              new Country() { Code = "ID", NameEn = "Indonesia", NameAr = "أندونيسيا", LanguageEn = "Indonesian", LanguageAr = "أندونيسيي" },
              new Country() { Code = "IR", NameEn = "Iran", NameAr = "إيران", LanguageEn = "Iranian", LanguageAr = "إيراني" },
              new Country() { Code = "IQ", NameEn = "Iraq", NameAr = "العراق", LanguageEn = "Iraqi", LanguageAr = "عراقي" },
              new Country() { Code = "IE", NameEn = "Ireland", NameAr = "إيرلندا", LanguageEn = "Irish", LanguageAr = "إيرلندي" },
              new Country() { Code = "IL", NameEn = "Israel", NameAr = "إسرائيل", LanguageEn = "Israeli", LanguageAr = "إسرائيلي" },
              new Country() { Code = "IT", NameEn = "Italy", NameAr = "إيطاليا", LanguageEn = "Italian", LanguageAr = "إيطالي" },
              new Country() { Code = "CI", NameEn = "Ivory Coast", NameAr = "ساحل العاج", LanguageEn = "Ivory Coastian", LanguageAr = "ساحل العاج" },
              new Country() { Code = "JE", NameEn = "Jersey", NameAr = "جيرزي", LanguageEn = "Jersian", LanguageAr = "جيرزي" },
              new Country() { Code = "JM", NameEn = "Jamaica", NameAr = "جمايكا", LanguageEn = "Jamaican", LanguageAr = "جمايكي" },
              new Country() { Code = "JP", NameEn = "Japan", NameAr = "اليابان", LanguageEn = "Japanese", LanguageAr = "ياباني" },
              new Country() { Code = "JO", NameEn = "Jordan", NameAr = "الأردن", LanguageEn = "Jordanian", LanguageAr = "أردني" },
              new Country() { Code = "KZ", NameEn = "Kazakhstan", NameAr = "كازاخستان", LanguageEn = "Kazakh", LanguageAr = "كازاخستاني" },
              new Country() { Code = "KE", NameEn = "Kenya", NameAr = "كينيا", LanguageEn = "Kenyan", LanguageAr = "كيني" },
              new Country() { Code = "KI", NameEn = "Kiribati", NameAr = "كيريباتي", LanguageEn = "I-Kiribati", LanguageAr = "كيريباتي" },
              new Country() { Code = "KP", NameEn = "Korea(North Korea)", NameAr = "كوريا الشمالية", LanguageEn = "North Korean", LanguageAr = "كوري" },
              new Country() { Code = "KR", NameEn = "Korea(South Korea)", NameAr = "كوريا الجنوبية", LanguageEn = "South Korean", LanguageAr = "كوري" },
              new Country() { Code = "XK", NameEn = "Kosovo", NameAr = "كوسوفو", LanguageEn = "Kosovar", LanguageAr = "كوسيفي" },
              new Country() { Code = "KW", NameEn = "Kuwait", NameAr = "الكويت", LanguageEn = "Kuwaiti", LanguageAr = "كويتي" },
              new Country() { Code = "KG", NameEn = "Kyrgyzstan", NameAr = "قيرغيزستان", LanguageEn = "Kyrgyzstani", LanguageAr = "قيرغيزستاني" },
              new Country() { Code = "LA", NameEn = "Lao PDR", NameAr = "لاوس", LanguageEn = "Laotian", LanguageAr = "لاوسي" },
              new Country() { Code = "LV", NameEn = "Latvia", NameAr = "لاتفيا", LanguageEn = "Latvian", LanguageAr = "لاتيفي" },
              new Country() { Code = "LB", NameEn = "Lebanon", NameAr = "لبنان", LanguageEn = "Lebanese", LanguageAr = "لبناني" },
              new Country() { Code = "LS", NameEn = "Lesotho", NameAr = "ليسوتو", LanguageEn = "Basotho", LanguageAr = "ليوسيتي" },
              new Country() { Code = "LR", NameEn = "Liberia", NameAr = "ليبيريا", LanguageEn = "Liberian", LanguageAr = "ليبيري" },
              new Country() { Code = "LY", NameEn = "Libya", NameAr = "ليبيا", LanguageEn = "Libyan", LanguageAr = "ليبي" },
              new Country() { Code = "LI", NameEn = "Liechtenstein", NameAr = "ليختنشتين", LanguageEn = "Liechtenstein", LanguageAr = "ليختنشتيني" },
              new Country() { Code = "LT", NameEn = "Lithuania", NameAr = "لتوانيا", LanguageEn = "Lithuanian", LanguageAr = "لتوانيي" },
              new Country() { Code = "LU", NameEn = "Luxembourg", NameAr = "لوكسمبورغ", LanguageEn = "Luxembourger", LanguageAr = "لوكسمبورغي" },
              new Country() { Code = "LK", NameEn = "Sri Lanka", NameAr = "سريلانكا", LanguageEn = "Sri Lankian", LanguageAr = "سريلانكي" },
              new Country() { Code = "MO", NameEn = "Macau", NameAr = "ماكاو", LanguageEn = "Macanese", LanguageAr = "ماكاوي" },
              new Country() { Code = "MK", NameEn = "Macedonia", NameAr = "مقدونيا", LanguageEn = "Macedonian", LanguageAr = "مقدوني" },
              new Country() { Code = "MG", NameEn = "Madagascar", NameAr = "مدغشقر", LanguageEn = "Malagasy", LanguageAr = "مدغشقري" },
              new Country() { Code = "MW", NameEn = "Malawi", NameAr = "مالاوي", LanguageEn = "Malawian", LanguageAr = "مالاوي" },
              new Country() { Code = "MY", NameEn = "Malaysia", NameAr = "ماليزيا", LanguageEn = "Malaysian", LanguageAr = "ماليزي" },
              new Country() { Code = "MV", NameEn = "Maldives", NameAr = "المالديف", LanguageEn = "Maldivian", LanguageAr = "مالديفي" },
              new Country() { Code = "ML", NameEn = "Mali", NameAr = "مالي", LanguageEn = "Malian", LanguageAr = "مالي" },
              new Country() { Code = "MT", NameEn = "Malta", NameAr = "مالطا", LanguageEn = "Maltese", LanguageAr = "مالطي" },
              new Country() { Code = "MH", NameEn = "Marshall Islands", NameAr = "جزر مارشال", LanguageEn = "Marshallese", LanguageAr = "مارشالي" },
              new Country() { Code = "MQ", NameEn = "Martinique", NameAr = "مارتينيك", LanguageEn = "Martiniquais", LanguageAr = "مارتينيكي" },
              new Country() { Code = "MR", NameEn = "Mauritania", NameAr = "موريتانيا", LanguageEn = "Mauritanian", LanguageAr = "موريتانيي" },
              new Country() { Code = "MU", NameEn = "Mauritius", NameAr = "موريشيوس", LanguageEn = "Mauritian", LanguageAr = "موريشيوسي" },
              new Country() { Code = "YT", NameEn = "Mayotte", NameAr = "مايوت", LanguageEn = "Mahoran", LanguageAr = "مايوتي" },
              new Country() { Code = "MX", NameEn = "Mexico", NameAr = "المكسيك", LanguageEn = "Mexican", LanguageAr = "مكسيكي" },
              new Country() { Code = "FM", NameEn = "Micronesia", NameAr = "مايكرونيزيا", LanguageEn = "Micronesian", LanguageAr = "مايكرونيزيي" },
              new Country() { Code = "MD", NameEn = "Moldova", NameAr = "مولدافيا", LanguageEn = "Moldovan", LanguageAr = "مولديفي" },
              new Country() { Code = "MC", NameEn = "Monaco", NameAr = "موناكو", LanguageEn = "Monacan", LanguageAr = "مونيكي" },
              new Country() { Code = "MN", NameEn = "Mongolia", NameAr = "منغوليا", LanguageEn = "Mongolian", LanguageAr = "منغولي" },
              new Country() { Code = "ME", NameEn = "Montenegro", NameAr = "الجبل الأسود", LanguageEn = "Montenegrin", LanguageAr = "الجبل الأسود" },
              new Country() { Code = "MS", NameEn = "Montserrat", NameAr = "مونتسيرات", LanguageEn = "Montserratian", LanguageAr = "مونتسيراتي" },
              new Country() { Code = "MA", NameEn = "Morocco", NameAr = "المغرب", LanguageEn = "Moroccan", LanguageAr = "مغربي" },
              new Country() { Code = "MZ", NameEn = "Mozambique", NameAr = "موزمبيق", LanguageEn = "Mozambican", LanguageAr = "موزمبيقي" },
              new Country() { Code = "MM", NameEn = "Myanmar", NameAr = "ميانمار", LanguageEn = "Myanmarian", LanguageAr = "ميانماري" },
              new Country() { Code = "NA", NameEn = "Namibia", NameAr = "ناميبيا", LanguageEn = "Namibian", LanguageAr = "ناميبي" },
              new Country() { Code = "NR", NameEn = "Nauru", NameAr = "نورو", LanguageEn = "Nauruan", LanguageAr = "نوري" },
              new Country() { Code = "NP", NameEn = "Nepal", NameAr = "نيبال", LanguageEn = "Nepalese", LanguageAr = "نيبالي" },
              new Country() { Code = "NL", NameEn = "Netherlands", NameAr = "هولندا", LanguageEn = "Dutch", LanguageAr = "هولندي" },
              new Country() { Code = "AN", NameEn = "Netherlands Antilles", NameAr = "جزر الأنتيل الهولندي", LanguageEn = "Dutch Antilier", LanguageAr = "هولندي" },
              new Country() { Code = "NC", NameEn = "New Caledonia", NameAr = "كاليدونيا الجديدة", LanguageEn = "New Caledonian", LanguageAr = "كاليدوني" },
              new Country() { Code = "NZ", NameEn = "New Zealand", NameAr = "نيوزيلندا", LanguageEn = "New Zealander", LanguageAr = "نيوزيلندي" },
              new Country() { Code = "NI", NameEn = "Nicaragua", NameAr = "نيكاراجوا", LanguageEn = "Nicaraguan", LanguageAr = "نيكاراجوي" },
              new Country() { Code = "NE", NameEn = "Niger", NameAr = "النيجر", LanguageEn = "Nigerien", LanguageAr = "نيجيري" },
              new Country() { Code = "NG", NameEn = "Nigeria", NameAr = "نيجيريا", LanguageEn = "Nigerian", LanguageAr = "نيجيري" },
              new Country() { Code = "NU", NameEn = "Niue", NameAr = "ني", LanguageEn = "Niuean", LanguageAr = "ني" },
              new Country() { Code = "NF", NameEn = "Norfolk Island", NameAr = "جزيرة نورفولك", LanguageEn = "Norfolk Islander", LanguageAr = "نورفوليكي" },
              new Country() { Code = "MP", NameEn = "Northern Mariana Islands", NameAr = "جزر ماريانا الشمالية", LanguageEn = "Northern Marianan", LanguageAr = "ماريني" },
              new Country() { Code = "NO", NameEn = "Norway", NameAr = "النرويج", LanguageEn = "Norwegian", LanguageAr = "نرويجي" },
              new Country() { Code = "OM", NameEn = "Oman", NameAr = "عمان", LanguageEn = "Omani", LanguageAr = "عماني" },
              new Country() { Code = "PK", NameEn = "Pakistan", NameAr = "باكستان", LanguageEn = "Pakistani", LanguageAr = "باكستاني" },
              new Country() { Code = "PW", NameEn = "Palau", NameAr = "بالاو", LanguageEn = "Palauan", LanguageAr = "بالاوي" },
              new Country() { Code = "PS", NameEn = "Palestine", NameAr = "فلسطين", LanguageEn = "Palestinian", LanguageAr = "فلسطيني" },
              new Country() { Code = "PA", NameEn = "Panama", NameAr = "بنما", LanguageEn = "Panamanian", LanguageAr = "بنمي" },
              new Country() { Code = "PG", NameEn = "Papua New Guinea", NameAr = "بابوا غينيا الجديدة", LanguageEn = "Papua New Guinean", LanguageAr = "بابوي" },
              new Country() { Code = "PY", NameEn = "Paraguay", NameAr = "باراغواي", LanguageEn = "Paraguayan", LanguageAr = "بارغاوي" },
              new Country() { Code = "PE", NameEn = "Peru", NameAr = "بيرو", LanguageEn = "Peruvian", LanguageAr = "بيري" },
              new Country() { Code = "PH", NameEn = "Philippines", NameAr = "الفليبين", LanguageEn = "Filipino", LanguageAr = "فلبيني" },
              new Country() { Code = "PN", NameEn = "Pitcairn", NameAr = "بيتكيرن", LanguageEn = "Pitcairn Islander", LanguageAr = "بيتكيرني" },
              new Country() { Code = "PL", NameEn = "Poland", NameAr = "بولندا", LanguageEn = "Polish", LanguageAr = "بولندي" },
              new Country() { Code = "PT", NameEn = "Portugal", NameAr = "البرتغال", LanguageEn = "Portuguese", LanguageAr = "برتغالي" },
              new Country() { Code = "PR", NameEn = "Puerto Rico", NameAr = "بورتو ريكو", LanguageEn = "Puerto Rican", LanguageAr = "بورتي" },
              new Country() { Code = "QA", NameEn = "Qatar", NameAr = "قطر", LanguageEn = "Qatari", LanguageAr = "قطري" },
              new Country() { Code = "RE", NameEn = "Reunion Island", NameAr = "ريونيون", LanguageEn = "Reunionese", LanguageAr = "ريونيوني" },
              new Country() { Code = "RO", NameEn = "Romania", NameAr = "رومانيا", LanguageEn = "Romanian", LanguageAr = "روماني" },
              new Country() { Code = "RU", NameEn = "Russian", NameAr = "روسيا", LanguageEn = "Russian", LanguageAr = "روسي" },
              new Country() { Code = "RW", NameEn = "Rwanda", NameAr = "رواندا", LanguageEn = "Rwandan", LanguageAr = "رواندا" },
              new Country() { Code = "KN", NameEn = "Saint Kitts and Nevis", NameAr = "سانت كيتس ونيفس,", LanguageEn = "Kittitian/Nevisian", LanguageAr = "سانت كيتس ونيفس" },
              new Country() { Code = "MF", NameEn = "Saint Martin (French part)", NameAr = "ساينت مارتن فرنسي", LanguageEn = "St. Martian(French)", LanguageAr = "ساينت مارتني فرنسي" },
              new Country() { Code = "SX", NameEn = "Sint Maarten (Dutch part)", NameAr = "ساينت مارتن هولندي", LanguageEn = "St. Martian(Dutch)", LanguageAr = "ساينت مارتني هولندي" },
              new Country() { Code = "LC", NameEn = "Saint Pierre and Miquelon", NameAr = "سان بيير وميكلون", LanguageEn = "St. Pierre and Miquelon", LanguageAr = "سان بيير وميكلوني" },
              new Country() { Code = "VC", NameEn = "Saint Vincent and the Grenadines", NameAr = "سانت فنسنت وجزر غرينادين", LanguageEn = "Saint Vincent and the Grenadines", LanguageAr = "سانت فنسنت وجزر غرينادين" },
              new Country() { Code = "WS", NameEn = "Samoa", NameAr = "ساموا", LanguageEn = "Samoan", LanguageAr = "ساموي" },
              new Country() { Code = "SM", NameEn = "San Marino", NameAr = "سان مارينو", LanguageEn = "Sammarinese", LanguageAr = "ماريني" },
              new Country() { Code = "ST", NameEn = "Sao Tome and Principe", NameAr = "ساو تومي وبرينسيبي", LanguageEn = "Sao Tomean", LanguageAr = "ساو تومي وبرينسيبي" },
              new Country() { Code = "SA", NameEn = "Saudi Arabia", NameAr = "المملكة العربية السعودية", LanguageEn = "Saudi Arabian", LanguageAr = "سعودي" },
              new Country() { Code = "SN", NameEn = "Senegal", NameAr = "السنغال", LanguageEn = "Senegalese", LanguageAr = "سنغالي" },
              new Country() { Code = "RS", NameEn = "Serbia", NameAr = "صربيا", LanguageEn = "Serbian", LanguageAr = "صربي" },
              new Country() { Code = "SC", NameEn = "Seychelles", NameAr = "سيشيل", LanguageEn = "Seychellois", LanguageAr = "سيشيلي" },
              new Country() { Code = "SL", NameEn = "Sierra Leone", NameAr = "سيراليون", LanguageEn = "Sierra Leonean", LanguageAr = "سيراليوني" },
              new Country() { Code = "SG", NameEn = "Singapore", NameAr = "سنغافورة", LanguageEn = "Singaporean", LanguageAr = "سنغافوري" },
              new Country() { Code = "SK", NameEn = "Slovakia", NameAr = "سلوفاكيا", LanguageEn = "Slovak", LanguageAr = "سولفاكي" },
              new Country() { Code = "SI", NameEn = "Slovenia", NameAr = "سلوفينيا", LanguageEn = "Slovenian", LanguageAr = "سولفيني" },
              new Country() { Code = "SB", NameEn = "Solomon Islands", NameAr = "جزر سليمان", LanguageEn = "Solomon Island", LanguageAr = "جزر سليمان" },
              new Country() { Code = "SO", NameEn = "Somalia", NameAr = "الصومال", LanguageEn = "Somali", LanguageAr = "صومالي" },
              new Country() { Code = "ZA", NameEn = "South Africa", NameAr = "جنوب أفريقيا", LanguageEn = "South African", LanguageAr = "أفريقي" },
              new Country() { Code = "GS", NameEn = "South Georgia and the South Sandwich", NameAr = "المنطقة القطبية الجنوبية", LanguageEn = "South Georgia and the South Sandwich", LanguageAr = "لمنطقة القطبية الجنوبية" },
              new Country() { Code = "SS", NameEn = "South Sudan", NameAr = "السودان الجنوبي", LanguageEn = "South Sudanese", LanguageAr = "سوادني جنوبي" },
              new Country() { Code = "ES", NameEn = "Spain", NameAr = "إسبانيا", LanguageEn = "Spanish", LanguageAr = "إسباني" },
              new Country() { Code = "SH", NameEn = "Saint Helena", NameAr = "سانت هيلانة", LanguageEn = "St. Helenian", LanguageAr = "هيلاني" },
              new Country() { Code = "SD", NameEn = "Sudan", NameAr = "السودان", LanguageEn = "Sudanese", LanguageAr = "سوداني" },
              new Country() { Code = "SR", NameEn = "Suriname", NameAr = "سورينام", LanguageEn = "Surinamese", LanguageAr = "سورينامي" },
              new Country() { Code = "SJ", NameEn = "Svalbard and Jan Mayen", NameAr = "سفالبارد ويان ماين", LanguageEn = "Svalbardian/Jan Mayenian", LanguageAr = "سفالبارد ويان ماين" },
              new Country() { Code = "SZ", NameEn = "Swaziland", NameAr = "سوازيلند", LanguageEn = "Swazi", LanguageAr = "سوازيلندي" },
              new Country() { Code = "SE", NameEn = "Sweden", NameAr = "السويد", LanguageEn = "Swedish", LanguageAr = "سويدي" },
              new Country() { Code = "CH", NameEn = "Switzerland", NameAr = "سويسرا", LanguageEn = "Swiss", LanguageAr = "سويسري" },
              new Country() { Code = "SY", NameEn = "Syria", NameAr = "سوريا", LanguageEn = "Syrian", LanguageAr = "سوري" },
              new Country() { Code = "TW", NameEn = "Taiwan", NameAr = "تايوان", LanguageEn = "Taiwanese", LanguageAr = "تايواني" },
              new Country() { Code = "TJ", NameEn = "Tajikistan", NameAr = "طاجيكستان", LanguageEn = "Tajikistani", LanguageAr = "طاجيكستاني" },
              new Country() { Code = "TZ", NameEn = "Tanzania", NameAr = "تنزانيا", LanguageEn = "Tanzanian", LanguageAr = "تنزانيي" },
              new Country() { Code = "TH", NameEn = "Thailand", NameAr = "تايلندا", LanguageEn = "Thai", LanguageAr = "تايلندي" },
              new Country() { Code = "TL", NameEn = "Timor-Leste", NameAr = "تيمور الشرقية", LanguageEn = "Timor-Lestian", LanguageAr = "تيموري" },
              new Country() { Code = "TG", NameEn = "Togo", NameAr = "توغو", LanguageEn = "Togolese", LanguageAr = "توغي" },
              new Country() { Code = "TK", NameEn = "Tokelau", NameAr = "توكيلاو", LanguageEn = "Tokelaian", LanguageAr = "توكيلاوي" },
              new Country() { Code = "TO", NameEn = "Tonga", NameAr = "تونغا", LanguageEn = "Tongan", LanguageAr = "تونغي" },
              new Country() { Code = "TT", NameEn = "Trinidad and Tobago", NameAr = "ترينيداد وتوباغو", LanguageEn = "Trinidadian/Tobagonian", LanguageAr = "ترينيداد وتوباغو" },
              new Country() { Code = "TN", NameEn = "Tunisia", NameAr = "تونس", LanguageEn = "Tunisian", LanguageAr = "تونسي" },
              new Country() { Code = "TR", NameEn = "Turkey", NameAr = "تركيا", LanguageEn = "Turkish", LanguageAr = "تركي" },
              new Country() { Code = "TM", NameEn = "Turkmenistan", NameAr = "تركمانستان", LanguageEn = "Turkmen", LanguageAr = "تركمانستاني" },
              new Country() { Code = "TC", NameEn = "Turks and Caicos Islands", NameAr = "جزر توركس وكايكوس", LanguageEn = "Turks and Caicos Islands", LanguageAr = "جزر توركس وكايكوس" },
              new Country() { Code = "TV", NameEn = "Tuvalu", NameAr = "توفالو", LanguageEn = "Tuvaluan", LanguageAr = "توفالي" },
              new Country() { Code = "UG", NameEn = "Uganda", NameAr = "أوغندا", LanguageEn = "Ugandan", LanguageAr = "أوغندي" },
              new Country() { Code = "UA", NameEn = "Ukraine", NameAr = "أوكرانيا", LanguageEn = "Ukrainian", LanguageAr = "أوكراني" },
              new Country() { Code = "AE", NameEn = "United Arab Emirates", NameAr = "الإمارات العربية المتحدة", LanguageEn = "Emirati", LanguageAr = "إماراتي" },
              new Country() { Code = "GB", NameEn = "United Kingdom", NameAr = "المملكة المتحدة", LanguageEn = "British", LanguageAr = "بريطاني" },
              new Country() { Code = "US", NameEn = "United States", NameAr = "الولايات المتحدة", LanguageEn = "American", LanguageAr = "أمريكي" },
              new Country() { Code = "UM", NameEn = "US Minor Outlying Islands", NameAr = "قائمة الولايات والمناطق الأمريكية", LanguageEn = "US Minor Outlying Islander", LanguageAr = "أمريكي" },
              new Country() { Code = "UY", NameEn = "Uruguay", NameAr = "أورغواي", LanguageEn = "Uruguayan", LanguageAr = "أورغواي" },
              new Country() { Code = "UZ", NameEn = "Uzbekistan", NameAr = "أوزباكستان", LanguageEn = "Uzbek", LanguageAr = "أوزباكستاني" },
              new Country() { Code = "VU", NameEn = "Vanuatu", NameAr = "فانواتو", LanguageEn = "Vanuatuan", LanguageAr = "فانواتي" },
              new Country() { Code = "VE", NameEn = "Venezuela", NameAr = "فنزويلا", LanguageEn = "Venezuelan", LanguageAr = "فنزويلي" },
              new Country() { Code = "VN", NameEn = "Vietnam", NameAr = "فيتنام", LanguageEn = "Vietnamese", LanguageAr = "فيتنامي" },
              new Country() { Code = "VI", NameEn = "Virgin Islands (U.S.)", NameAr = "الجزر العذراء الأمريكي", LanguageEn = "American Virgin Islander", LanguageAr = "أمريكي" },
              new Country() { Code = "VA", NameEn = "Vatican City", NameAr = "فنزويلا", LanguageEn = "Vatican", LanguageAr = "فاتيكاني" },
              new Country() { Code = "WF", NameEn = "Wallis and Futuna Islands", NameAr = "والس وفوتونا", LanguageEn = "Wallisian/Futunan", LanguageAr = "فوتوني" },
              new Country() { Code = "EH", NameEn = "Western Sahara", NameAr = "الصحراء الغربية", LanguageEn = "Sahrawian", LanguageAr = "صحراوي" },
              new Country() { Code = "YE", NameEn = "Yemen", NameAr = "اليمن", LanguageEn = "Yemeni", LanguageAr = "يمني" },
              new Country() { Code = "ZM", NameEn = "Zambia", NameAr = "زامبيا", LanguageEn = "Zambian", LanguageAr = "زامبياني" },
              new Country() { Code = "ZW", NameEn = "Zimbabwe", NameAr = "زمبابوي", LanguageEn = "Zimbabwean", LanguageAr = "زمبابوي" });

            await _dbContext.SaveChangesAsync();
        }


    }
}
