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
using DocumentFormat.OpenXml.Spreadsheet;
using Mono.TextTemplating;

namespace HolyQuran.Infrastructure.Services.Chapters
{
    public class ChapterService : IChapterService
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly IMapper _mapper;
        public ChapterService(ApplicationDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public PagingAPIViewModel GetAll(int page)
        {
            var pageSize = 10;
            var totalmodels = _dbContext.Chapters.Count();
            var totalPages = (int)Math.Ceiling(totalmodels / (double)pageSize);
            if (totalPages != 0)
            {
                // Ensure page number is within valid range
                page = Math.Clamp(page, 1, totalPages);
            }
            var skipCount = (page - 1) * pageSize;

            IQueryable<Chapter> query = _dbContext.Chapters
                .Skip(skipCount)
                .Take(pageSize);



            var modelquery = query.ToList();
            var modelViewModels = _mapper.Map<List<ChapterViewModel>>(modelquery);
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
            await _dbContext.Chapters.AddRangeAsync(
new Chapter() { Arabic = "سورة الفاتحة", Latin = "Al-Fatiha", English = "The Opening", Localtion = "1", Sajda = "0", Ayah = 7 },
new Chapter() { Arabic = "سورة البقرة", Latin = "Al-Baqara", English = "The Cow", Localtion = "2", Sajda = "0", Ayah = 286 },
new Chapter() { Arabic = "سورة آل عمران", Latin = "Aal-e-Imran", English = "The family of Imran", Localtion = "2", Sajda = "0", Ayah = 200 },
new Chapter() { Arabic = "سورة النساء", Latin = "An-Nisa", English = "The Women", Localtion = "2", Sajda = "0", Ayah = 176 },
new Chapter() { Arabic = "سورة المائدة", Latin = "Al-Maeda", English = "The Table Spread", Localtion = "2", Sajda = "0", Ayah = 120 },
new Chapter() { Arabic = "سورة الأنعام", Latin = "Al-Anaam", English = "The cattle", Localtion = "1", Sajda = "0", Ayah = 165 }
);
//new Chapter() { Arabic = "سورة الأعراف", "Al-Araf", "The heights", "1", "206", 206) },
//new Chapter() { Arabic = "سورة الأنفال", "Al-Anfal", "Spoils of war, booty", "2", "0", 75) },
//new Chapter() { Arabic = "سورة التوبة", "At-Taubah", "Repentance", "2", "0", 129) },
//new Chapter() { Arabic = "سورة يونس", "Yunus", "Jonah", "1", "0", 109) },
//new Chapter() { Arabic = "سورة هود", "Hud", "Hud", "1", "0", 123) },
//new Chapter() { Arabic = "سورة يوسف", "Yusuf", "Joseph", "1", "0", 111) },
//new Chapter() { Arabic = "سورة الرعد", "Ar-Rad", "The Thunder", "1", "15", 43) },
//new Chapter() { Arabic = "سورة إبراهيم", "Ibrahim", "Abraham", "1", "0", 52) },
//new Chapter() { Arabic = "سورة الحجر", "Al-Hijr", "Stoneland, Rock city, Al-Hijr valley", "1", "0", 99) },
//new Chapter() { Arabic = "سورة النحل", "An-Nahl", "The Bee", "1", "50", 128) },
//new Chapter() { Arabic = "سورة الإسراء", "Al-Isra", "The night journey", "1", "100", 111) },
//new Chapter() { Arabic = "سورة الكهف", "Al-Kahf", "The cave", "1", "0", 110) },
//new Chapter() { Arabic = "سورة مريم", "Maryam", "Mary", "1", "58", 98) },
//new Chapter() { Arabic = "سورة طه", "Taha", "Taha", "1", "0", 135) },
//new Chapter() { Arabic = "سورة الأنبياء", "Al-Anbiya", "The Prophets", "1", "0", 112) },
//new Chapter() { Arabic = "سورة الحج", "Al-Hajj", "The Pilgrimage", "1", "18", 78) },
//new Chapter() { Arabic = "سورة المؤمنون", "Al-Mumenoon", "The Believers", "1", "0", 118) },
//new Chapter() { Arabic = "سورة النور", "An-Noor", "The Light", "1", "0", 64) },
//new Chapter() { Arabic = "سورة الفرقان", "Al-Furqan", "The Standard", "1", "60", 77) },
//new Chapter() { Arabic = "سورة الشعراء", "Ash-Shuara", "The Poets", "1", "0", 227) },
//new Chapter() { Arabic = "سورة النمل", "An-Naml", "THE ANT", "1", "26", 93) },
//new Chapter() { Arabic = "سورة القصص", "Al-Qasas", "The Story", "1", "0", 88) },
//new Chapter() { Arabic = "سورة العنكبوت", "Al-Ankaboot", "The Spider", "1", "0", 69) },
//new Chapter() { Arabic = "سورة الروم", "Ar-Room", "The Romans", "1", "0", 60) },
//new Chapter() { Arabic = "سورة لقمان", "Luqman", "Luqman", "1", "0", 34) },
//new Chapter() { Arabic = "سورة السجدة", "As-Sajda", "The Prostration", "1", "15", 30) },
//new Chapter() { Arabic = "سورة الأحزاب", "Al-Ahzab", "The Coalition", "1", "0", 73) },
//new Chapter() { Arabic = "سورة سبأ", "Saba", "Saba", "1", "0", 54) },
//new Chapter() { Arabic = "سورة فاطر", "Fatir", "Originator", "1", "0", 45) },
//new Chapter() { Arabic = "سورة يس", "Ya Seen", "Ya Seen", "1", "0", 83) },
//new Chapter() { Arabic = "سورة الصافات", "As-Saaffat", "Those who set the ranks", "1", "0", 182) },
//new Chapter() { Arabic = "سورة ص", "Sad", "Sad", "1", "24", 88) },
//new Chapter() { Arabic = "سورة الزمر", "Az-Zumar", "The Troops", "1", "0", 75) },
//new Chapter() { Arabic = "سورة غافر", "Ghafir", "The Forgiver", "1", "0", 85) },
//new Chapter() { Arabic = "سورة فصلت", "Fussilat", "Explained in detail", "1", "38", 54) },
//new Chapter() { Arabic = "سورة الشورى", "Ash-Shura", "Council, Consultation", "1", "0", 53) },
//new Chapter() { Arabic = "سورة الزخرف", "Az-Zukhruf", "Ornaments of Gold", "1", "0", 89) },
//new Chapter() { Arabic = "سورة الدخان", "Ad-Dukhan", "The Smoke", "1", "0", 59) },
//new Chapter() { Arabic = "سورة الجاثية", "Al-Jathiya", "Crouching", "1", "0", 37) },
//new Chapter() { Arabic = "سورة الأحقاف", "Al-Ahqaf", "The wind-curved sandhills", "1", "0", 35) },
//new Chapter() { Arabic = "سورة محمد", "Muhammad", "Muhammad", "2", "0", 38) },
//new Chapter() { Arabic = "سورة الفتح", "Al-Fath", "The victory", "2", "0", 29) },
//new Chapter() { Arabic = "سورة الحجرات", "Al-Hujraat", "The private apartments", "2", "0", 18) },
//new Chapter() { Arabic = "سورة ق", "Qaf", "Qaf", "1", "0", 45) },
//new Chapter() { Arabic = "سورة الذاريات", "Adh-Dhariyat", "The winnowing winds", "1", "0", 60) },
//new Chapter() { Arabic = "سورة الطور", "At-tur", "Mount Sinai", "1", "0", 49) },
//new Chapter() { Arabic = "سورة النجم", "An-Najm", "The Star", "1", "62", 62) },
//new Chapter() { Arabic = "سورة القمر", "Al-Qamar", "The moon", "1", "0", 55) },
//new Chapter() { Arabic = "سورة الرحمن", "Al-Rahman", "The Beneficient", "1", "0", 78) },
//new Chapter() { Arabic = "سورة الواقعة", "Al-Waqia", "The Event, The Inevitable", "1", "0", 96) },
//new Chapter() { Arabic = "سورة الحديد", "Al-Hadid", "The Iron", "2", "0", 29) },
//new Chapter() { Arabic = "سورة المجادلة", "Al-Mujadila", "She that disputes", "2", "0", 22) },
//new Chapter() { Arabic = "سورة الحشر", "Al-Hashr", "Exile", "2", "0", 24) },
//new Chapter() { Arabic = "سورة الممتحنة", "Al-Mumtahina", "She that is to be examined", "2", "0", 13) },
//new Chapter() { Arabic = "سورة الصف", "As-Saff", "The Ranks", "2", "0", 14) },
//new Chapter() { Arabic = "سورة الجمعة", "Al-Jumua", "The congregation, Friday", "2", "0", 11) },
//new Chapter() { Arabic = "سورة المنافقون", "Al-Munafiqoon", "The Hypocrites", "2", "0", 11) },
//new Chapter() { Arabic = "سورة التغابن", "At-Taghabun", "Mutual Disillusion", "2", "0", 18) },
//new Chapter() { Arabic = "سورة الطلاق", "At-Talaq", "Divorce", "2", "0", 12) },
//new Chapter() { Arabic = "سورة التحريم", "At-Tahrim", "Banning", "2", "0", 12) },
//new Chapter() { Arabic = "سورة الملك", "Al-Mulk", "The Sovereignty", "1", "0", 30) },
//new Chapter() { Arabic = "سورة القلم", "Al-Qalam", "The Pen", "1", "0", 52) },
//new Chapter() { Arabic = "سورة الحاقة", "Al-Haaqqa", "The reality", "1", "0", 52) },
//new Chapter() { Arabic = "سورة المعارج", "Al-Maarij", "The Ascending stairways", "1", "0", 44) },
//new Chapter() { Arabic = "سورة نوح", "Nooh", "Nooh", "1", "0", 28) },
//new Chapter() { Arabic = "سورة الجن", "Al-Jinn", "The Jinn", "1", "0", 28) },
//new Chapter() { Arabic = "سورة المزمل", "Al-Muzzammil", "The enshrouded one", "1", "0", 20) },
//new Chapter() { Arabic = "سورة المدثر", "Al-Muddathir", "The cloaked one", "1", "0", 56) },
//new Chapter() { Arabic = "سورة القيامة", "Al-Qiyama", "The rising of the dead", "1", "0", 40) },
//new Chapter() { Arabic = "سورة الإنسان", "Al-Insan", "The man", "2", "0", 31) },
//new Chapter() { Arabic = "سورة المرسلات", "Al-Mursalat", "The emissaries", "1", "0", 50) },
//new Chapter() { Arabic = "سورة النبأ", "An-Naba", "The tidings", "1", "0", 40) },
//new Chapter() { Arabic = "سورة النازعات", "An-Naziat", "Those who drag forth", "1", "0", 46) },
//new Chapter() { Arabic = "سورة عبس", "Abasa", "He Frowned", "1", "0", 42) },
//new Chapter() { Arabic = "سورة التكوير", "At-Takwir", "The Overthrowing", "1", "0", 29) },
//new Chapter() { Arabic = "سورة الإنفطار", "AL-Infitar", "The Cleaving", "1", "0", 19) },
//new Chapter() { Arabic = "سورة المطففين", "Al-Mutaffifin", "Defrauding", "1", "0", 36) },
//new Chapter() { Arabic = "سورة الانشقاق", "Al-Inshiqaq", "The Sundering, Splitting Open", "1", "21", 25) },
//new Chapter() { Arabic = "سورة البروج", "Al-Burooj", "The Mansions of the stars", "1", "0", 22) },
//new Chapter() { Arabic = "سورة الطارق", "At-Tariq", "The morning star", "1", "0", 17) },
//new Chapter() { Arabic = "سورة الأعلى", "Al-Ala", "The Most High", "1", "0", 19) },
//new Chapter() { Arabic = "سورة الغاشية", "Al-Ghashiya", "The Overwhelming", "1", "0", 26) },
//new Chapter() { Arabic = "سورة الفجر", "Al-Fajr", "The Dawn", "1", "0", 30) },
//new Chapter() { Arabic = "سورة البلد", "Al-Balad", "The City", "1", "0", 20) },
//new Chapter() { Arabic = "سورة الشمس", "Ash-Shams", "The Sun", "1", "0", 15) },
//new Chapter() { Arabic = "سورة الليل", "Al-Lail", "The night", "1", "0", 21) },
//new Chapter() { Arabic = "سورة الضحى", "Ad-Dhuha", "The morning hours", "1", "0", 11) },
//new Chapter() { Arabic = "سورة الشرح", "Al-Inshirah", "Solace", "1", "0", 8) },
//new Chapter() { Arabic = "سورة التين", "At-Tin", "The Fig", "1", "0", 8) },
//new Chapter() { Arabic = "سورة العلق", "Al-Alaq", "The Clot", "1", "19", 19) },
//new Chapter() { Arabic = "سورة القدر", "Al-Qadr", "The Power", "1", "0", 5) },
//new Chapter() { Arabic = "سورة البينة", "Al-Bayyina", "The Clear proof", "2", "0", 8) },
//new Chapter() { Arabic = "سورة الزلزلة", "Al-Zalzala", "The earthquake", "2", "0", 8) },
//new Chapter() { Arabic = "سورة العاديات", "Al-Adiyat", "The Chargers", "1", "0", 11) },
//new Chapter() { Arabic = "سورة القارعة", "Al-Qaria", "The Calamity", "1", "0", 11) },
//new Chapter() { Arabic = "سورة التكاثر", "At-Takathur", "Competition", "1", "0", 8) },
//new Chapter() { Arabic = "سورة العصر", "Al-Asr", "The declining day", "1", "0", 3) },
//new Chapter() { Arabic = "سورة الهمزة", "Al-Humaza", "The Traducer", "1", "0", 9) },
//new Chapter() { Arabic = "سورة الفيل", "Al-fil", "The Elephant", "1", "0", 5) },
//new Chapter() { Arabic = "سورة قريش", "Quraish", "Quraish", "1", "0", 4) },
//new Chapter() { Arabic = "سورة الماعون", "Al-Maun", "Alms Giving", "1", "0", 7) },
//new Chapter() { Arabic = "سورة الكوثر", "Al-Kauther", "Abundance", "1", "0", 3) },
//new Chapter() { Arabic = "سورة الكافرون", "Al-Kafiroon", "The Disbelievers", "1", "0", 6) },
//new Chapter() { Arabic = "سورة النصر", "An-Nasr", "The Succour", "2", "0", 3) },
//new Chapter() { Arabic = "سورة المسد", "Al-Masadd", "The Flame", "1", "0", 5) },
//new Chapter() { Arabic = "سورة الإخلاص", "Al-Ikhlas", "Absoluteness", "1", "0", 4) },
//new Chapter() { Arabic = "سورة الفلق", "Al-Falaq", "The day break", "1", "0", 5) },
//new Chapter() { Arabic = "سورة الناس", "An-Nas", "The mankind", "1", "0", 6) };
        await _dbContext.SaveChangesAsync();
    }


}
}
