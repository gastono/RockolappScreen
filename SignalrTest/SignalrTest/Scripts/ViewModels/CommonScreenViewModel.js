var CommonScreenViewModel = function (spotifyUserId) {
    var self = this;

    self.spotifyUserId = ko.observable(spotifyUserId);
    self.CurrentTimer = ko.observable();
    self.ScreenData = new RockolappScreenData();
    self.IntervalId = ko.observable();
    GetCurrentTrack(this);
}

function setTime(context)
{
    var self = context;   

    if (self.IntervalId != undefined || self.IntervalId() != '' || self.IntervalId() != undefined) {
        window.clearInterval(self.IntervalId());
    }
    //setInterval(GetCurrentTrack(context), self.CurrentTimer());

   var id =  window.setInterval(function () {

        GetCurrentTrack(context)
   }, 15000);

   self.IntervalId(id);
}

function GetCurrentTrack(context) {
    var self = context;

    $.ajax({
        url: "/api/ScreenApi/GetCurrentTrack",
        type: 'GET',
        data: { spotifyUserId: self.spotifyUserId() },
        success: function (data) {           
            if (data != undefined) {
                self.ScreenData.Artist(data.Artist);
                self.ScreenData.ImageUrl(data.ImageUrl);
                self.ScreenData.TrackName(data.TrackName);
                self.ScreenData.Duration(data.Duration);
                self.ScreenData.LapsedTime(data.LapsedTime);
            }
           

            var remainingTime = data.Duration - data.LapsedTime - 10;

            self.CurrentTimer(remainingTime);

            setTime(self);

        }
    })
}


var RockolappScreenData = function () {
    var self = this;

    self.Artist = ko.observable();
    self.ImageUrl = ko.observable();
    self.TrackName = ko.observable();
    self.Duration = ko.observable();
    self.LapsedTime = ko.observable();
}