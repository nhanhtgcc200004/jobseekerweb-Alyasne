using EnterpriceWeb.Mailutils;
using finalyearproject.Models;
using finalyearproject.SubSystem.Mailutils;
using System.IO.Compression;

namespace EnterpriceWeb.Controllers
{
    public class SendMailSystem
    {
        private readonly IEmailSender _emailSender;
        private readonly IWebHostEnvironment _hostEnvironment;

        public SendMailSystem(IEmailSender emailSender, IWebHostEnvironment hostEnvironment)
        {
            _emailSender = emailSender;
            _hostEnvironment = hostEnvironment;
        }
       
        public string randomPass()
        {
            Random rand = new Random();

            // Choosing the size of string 
            // Using Next() string 
            int stringlen = rand.Next(4, 10);
            int randValue;
            string str = "";
            char letter;
            for (int i = 0; i < stringlen; i++)
            {

                // Generating a random number. 
                randValue = rand.Next(0, 26);

                // Generating random character by converting 
                // the random number into character. 
                letter = Convert.ToChar(randValue + 65);

                // Appending the letter to string. 
                str = str + letter;
            }
            return str;
        }
        public async Task<string> SendgmailForgetPassword(string gmail)
        {
            var receiver = gmail;
            string newpassword = randomPass();
            var subject = "there is your new password please don't tell someone else";
            var message = "there is your new password "+ newpassword;
            try
            {
                await _emailSender.SenderEmailAsync(receiver, subject, message);
                return newpassword;
            }
            catch (Exception ex)
            {
                return "Something wrong with your account";
            }

        }

        public async Task<string> SendMailAcceptReport(User reporter,User reciver,Report report)
        {
            var reporter_email = reporter.Email;
            var reciver_email = reciver.Email;
            var subject = "Your post was be reported By "+reporter.Name;
            var message = "your post at "+report.date_submit+"was be reported with reason "+report.content_report;
            var subject_reporter = "your report was be accepted";
            var message_reporter = "thank your contribution the post "+ report.post_id+"was be deleted";
            try
            {
                await _emailSender.SenderEmailAsync(reciver_email, subject, message);
                await _emailSender.SenderEmailAsync(reporter_email, subject_reporter, message_reporter);
                return "mail sent success";

            }
            catch (Exception ex)
            {
                return "Something wrong with your account";
            }
        }
        public async Task<string> SendgmailRefuseReport(string gmail)
        {
            var receiver = gmail;
            string newpassword = randomPass();
            var subject = "your report was be refused";
            var message = "follow our investigate your report have not enough evidence";
            try
            {
                await _emailSender.SenderEmailAsync(receiver, subject, message);
                return newpassword;
            }
            catch (Exception ex)
            {
                return "Something wrong with your account";
            }

        }

        public async void SendVerifyCode(string verify_code,string gmail)
        {
            var receiver = gmail;
            var subject = "This mail be sent by our website to confirm your mail please enter the verify code " +
                "to the website to finish complete create your create account";
            var message = "Your verify code is "+verify_code;
            try
            {
                await _emailSender.SenderEmailAsync(receiver, subject, message);
            }
            catch (Exception ex)
            {

            }
        }
        public MemoryStream DownloadSingleFile(CV cV)
        {
            // Create a memory stream to store the ZIP archive
            var memoryStream = new MemoryStream();

            // Create a zip archive
            using (var archive = new ZipArchive(memoryStream, ZipArchiveMode.Create, true))
            {
                
                    // Construct the path to the file
                    var filePath = Path.Combine(_hostEnvironment.WebRootPath, "Resume",  cV.cv_file);

                    if (System.IO.File.Exists(filePath))
                    {
                        // Create a new entry in the zip archive
                        var entry = archive.CreateEntry(cV.user.Name);

                        // Open the file and copy its contents into the zip entry
                        using (var fileStream = new FileStream(filePath, FileMode.Open))
                        using (var entryStream = entry.Open())
                        {
                            fileStream.CopyTo(entryStream);
                        }
                    }
                }

            // Reset the memory stream position
            memoryStream.Position = 0;

            // Return the ZIP archive as a file
            return memoryStream;
        }
        //public async Task<MemoryStream> DownloadProcessAsync(List<MemoryStream> memoryStreams, List<string> titles)
        //{
        //    var finalMemoryStream = new MemoryStream();

        //    using (var finalArchive = new ZipArchive(finalMemoryStream, ZipArchiveMode.Create, true))
        //    {
        //        for (int i = 0; i < memoryStreams.Count; i++)
        //        {
        //            // Reset the position of the memory stream to the beginning
        //            memoryStreams[i].Position = 0;

        //            // Create a zip entry for each memory stream
        //            var entry = finalArchive.CreateEntry($"+"+titles[i]+".zip");

        //            // Copy the content of the memory stream into the zip entry
        //            using (var entryStream = entry.Open())
        //            {
        //                await memoryStreams[i].CopyToAsync(entryStream);
        //            }
        //        }
        //    }

        //    // Reset the final memory stream position
        //    finalMemoryStream.Position = 0;

        //    // Return the final ZIP archive as a file
        //    return finalMemoryStream;
        //}
    }
}

