﻿    document.querySelector('.starrating').addEventListener('mouseover', function(e) {
        // console.log(e.target);
        switch (e.target.id) {
        case "img5":
        {

            document.getElementById('img5').style.display = "none";
            document.getElementById('img5_on').style.display = "inline";

        }
        case "img4":
        {
            document.getElementById('img4').style.display = "none";
            document.getElementById('img4_on').style.display = "inline";
        }
        case "img3":
        {

            document.getElementById('img3').style.display = "none";
            document.getElementById('img3_on').style.display = "inline";
        }
        case "img2":
        {
            document.getElementById('img2').style.display = "none";
            document.getElementById('img2_on').style.display = "inline";
        }
        case "img1":
        {
            document.getElementById('img1').style.display = "none";
            document.getElementById('img1_on').style.display = "inline";
            break;

        }
        default:
        }
        e.target.addEventListener('mouseout', function outhandler(d) {
            //  console.log(d.target);
            switch (d.target.id) {
            case "img5":
            {

                document.getElementById('img5').style.display = "inline";
                document.getElementById('img5_on').style.display = "none";

            }
            case "img4":
            {
                document.getElementById('img4').style.display = "inline";
                document.getElementById('img4_on').style.display = "none";
            }
            case "img3":
            {

                document.getElementById('img3').style.display = "inline";
                document.getElementById('img3_on').style.display = "none";
            }
            case "img2":
            {
                document.getElementById('img2').style.display = "inline";
                document.getElementById('img2_on').style.display = "none";
            }
            case "img1":
            {
                document.getElementById('img1').style.display = "inline";
                document.getElementById('img1_on').style.display = "none";
                break;

            }
            default:
            }
            e.target.removeEventListener('mouseout', outhandler, false);
        }, false);
    }, false);
    document.querySelector('.starrating').addEventListener('mousedown', function(e) {
        console.log(e.target);
        console.log(e.target.id);


        var rateValue;
        var reid = @recipetemp.Id;
        switch (e.target.id) {
        case "img5_on":
        case "img5":
        {
            rateValue = 5;
            break;
        }
        case "img4":
        case "img4_on":
        {
            rateValue = 4;
            break;
        }
        case "img3":
        case "img3_on":
        {
            rateValue = 3;
            break;
        }
        case "img2":
        case "img2_on":
        {
            rateValue = 2;
            break;
        }
        case "img1":
        case "img1_on":
        {
            rateValue = 1;
            break;

        }
       
        }
          
        $.ajax({
                type: "POST",
                //url: form.attr('action'),
                url:"@Url.Action("Rate","Recipe")",
         
                data: {
                    ReId: reid,
                    RateValue: rateValue,
                }
//                data: form.serialize()
            })
            .success(function (data) {
                //alert("your rate has been submitted");
                var template = $('#current-rate-template').clone().html();
                var html =
                    template
                   
                        .replace('{{FRate}}',data.FRate1)
                        .replace('{{NNum}}', data.NNum);


                $('.current-rate').replaceWith(html);
            })
            .error(function () {
                alert("your rate has been rejected");
            });

           
    });
    ///////////////////////////////
     document.querySelector('.renewPeopleNumber').addEventListener('click', function(e) {
        console.log(e.target);
   
                var form = $(this).parent("form");

                $.ajax({
                        type: "POST",
                        url: form.attr('action'),
                        data: form.serialize()
                    })
                    .success(function(data) {
                    var list = data.newamountlist;
                         console.log(list);
                       if(list)
                         {
                         console.log(e.target.className);
                        console.log(list);
                       $.each(list, function (index, item) {
                       var counter = '#amounttichange' + index;
                          $(counter).text(item);});
                       } 
                        else {
                       console.log(e.target.className);
                             $('#viewtoreplace').html(data);
                         }
                      
  
                    })
                    .error(function() {
                        alert("your rate has been rejected");
                    });

    });

