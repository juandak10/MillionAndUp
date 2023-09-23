using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var script = @"BEGIN TRANSACTION; 
BEGIN TRY

DECLARE @IdCountry UNIQUEIDENTIFIER SET @IdCountry = NEWID()
INSERT INTO Countries VALUES (@IdCountry,'United States','1','US')
DECLARE @IdState UNIQUEIDENTIFIER SET @IdState = NEWID()
INSERT INTO [States] VALUES (@IdState,'Florida','FL',@IdCountry)
DECLARE @IdCity UNIQUEIDENTIFIER SET @IdCity = NEWID()
INSERT INTO Cities VALUES (@IdCity,'Miami','MIA',@IdState)
DECLARE @IdZone1 UNIQUEIDENTIFIER SET @IdZone1= NEWID()
INSERT INTO Zones VALUES (@IdZone1,'Miami Beach','Miami Beach is an island city in South Florida, which is connected through bridges with the territory of Miami.',25.8102247,-80.2101818,@IdCity)
DECLARE @IdZone2 UNIQUEIDENTIFIER SET @IdZone2=NEWID()
INSERT INTO Zones VALUES (@IdZone2,'Surfside','Surfside is a town located in Miami-Dade County in the US state of Florida.',25.8794719,-80.1440787,@IdCity)
DECLARE @IdZone3 UNIQUEIDENTIFIER SET @IdZone3=NEWID()
INSERT INTO Zones VALUES (@IdZone3,'Sunny Isles','Sunny Isles Beach is a city located on a barrier island in northeastern Miami-Dade County in the US state of Florida.',25.9391081,-80.1425268,@IdCity)

DECLARE @IdAccount1 UNIQUEIDENTIFIER SET @IdAccount1=NEWID()
INSERT INTO Accounts VALUES (@IdAccount1,'Juan Vasquez','3200 Collins Ave, Miami Beach, FL 33140, EE. UU.','7867961369','juandak10@gmail.com','Janury5+','https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Account%2F334d95a4-f4cd-4b31-9af3-aaeab7aa3006.jpg?alt=media&token=d804d9b2-9c5a-41e0-b334-deb6e0d1cddf',CAST ('1992-06-05 20:44:11' AS DATETIME),1,2,GETDATE(),GETDATE(),1)
DECLARE @IdAccount2 UNIQUEIDENTIFIER SET @IdAccount2=NEWID()
INSERT INTO Accounts VALUES (@IdAccount2,'Marcela Higinio','8098-8000 Collins Ave, Miami Beach, FL 33141, EE. UU.','7863503856','marcelahy@gmail.com','October31*','https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Account%2F271ce34b-8be1-4fcb-bb28-ef0502909f45.jpg?alt=media&token=52e427f9-5cd5-4449-be9a-cb1b57bbe934',CAST ('1992-11-25 20:44:11' AS DATETIME),1,2,GETDATE(),GETDATE(),1)
DECLARE @IdAccount3 UNIQUEIDENTIFIER SET @IdAccount3=NEWID()
INSERT INTO Accounts VALUES (@IdAccount3,'Jade Signature','16901 Collins Ave, Sunny Isles Beach, FL 33160, EE. UU.','7864229847','jadesignature@gmail.com','June123+','https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Account%2F541ce34b-8be1-4fcb-bb28-ef0502909f45.jpeg?alt=media&token=7f540730-b758-4e78-b412-00cddeb5a766',CAST ('2014-12-28 20:44:11' AS DATETIME),2,1,GETDATE(),GETDATE(),1)

DECLARE @IdProperty UNIQUEIDENTIFIER 
SET @IdProperty=NEWID()
INSERT INTO Properties VALUES (@IdProperty,'Furnished house in COLLINS AVE','IMPECCABLY DESIGNED ART DECO PENTHOUSE MASTERPIECE BY WETZELS BROWN PARTNERS OF AMSTERDAM CROWNS FAENA HOUSE...ONE OF THE MOST EXCLUSIVE BOUTIQUE TOWERS IN MIAMI BEACH CREATED BY FOSTER + PARTNERS! Mansion in the Sky w/ 270° Views of Ocean, City & Bay. 6 Beds + 6.5 Baths in 6,400 SF of Living Space & Nearly 4,000 SF of Exterior Space. Private Elevator Foyer Arrival into Fine Art Gallery. Nanz Ebony Handles Greet you w/ Ocean Views. Italian Terrazzo Floors t/o Entertaining Areas w/ Dining for 14 Guests. High-Gloss Ebony Bookcase by Metrica + LED Lighting. Molteni Gourmet Eat-in Kitchen + Miele Appliances. Oceanfront Master Suite w/ Custom Built Ebony Furnishings. Rare Textured & Hand-Painted Walls in All Bedrooms. All Baths equipped w/ Dornbracht & Duravit Fixtures. Crestron Home Automation.','3315 COLLINS AVE #PH-A,MIAMI BEACH,FL 33140',25.81,-80.12, 2020, 250000, 1, GETDATE(), GETDATE(), 1, 2, 1, 1, 1, 3, 2, 2500, 2, 1, 1, 1, 1, 5, 2,@IdZone1,@IdAccount1)
INSERT INTO PropertyImages VALUES 
(NEWID(),'https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Property%2Fhouse1-1.png?alt=media&token=144dc7d1-7463-42b0-ae54-809b43f86dae',1,@IdProperty),
(NEWID(),'https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Property%2Fhouse1-2.png?alt=media&token=ee9f4775-ca26-487e-80bf-83ac4d6ee8fd',1,@IdProperty),
(NEWID(),'https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Property%2Fhouse1-3.png?alt=media&token=eca642f8-6fdf-46d6-93bb-dfa486635f63',1,@IdProperty)
INSERT INTO PropertyTraces VALUES (NEWID(),GETDATE(),'First Sale',30500000,500000,@IdAccount1,@IdAccount3,@IdProperty,GETDATE())

SET @IdProperty=NEWID()
INSERT INTO Properties VALUES (@IdProperty,'Apartment, Blue Diamond Residence in COLLINS AVE','Spectacular unobstructed, ocean and city views from every room in this unique Blue Diamond Residence. This unit combines ocean front lines 01 and 02 creating a 5 bedrooms/5 baths apartment totaling 3510 sq. feet. 2 master bedrooms with 2 master bathrooms + Jacuzzi tubs, floor to ceiling windows, 3 terraces, open kitchen, marble floors, spacious rooms, walk in closets and much more a must see. Great for entertaining from sun up to sun down with second kitchen off family room. 1st class amenities: 24hr security, valet, concierge, tennis, pool & towel service, cafe/market w/room service, a 16,000sf oceanside clubhouse/spa, gym, personal trainers, free workout classes, steam/ saunas, hot tubs, card & party rooms, nursery, playroom, billiards, business center, beach attendant, and more!','4779 COLLINS AVE #3201/3202,MIAMI BEACH,FL 33140',25.82,-80.12, 2019, 300000, 1, GETDATE(), GETDATE(), 2, 3, 2, 2, 0, 4, 3, 3000, 1, 0, 1, 0, 1, 6, 3,@IdZone1,@IdAccount2)
INSERT INTO PropertyImages VALUES 
(NEWID(),'https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Property%2FApt1-1.png?alt=media&token=8061534a-a564-4438-96c2-e16a49ee5338',1,@IdProperty),
(NEWID(),'https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Property%2FApt1-2.png?alt=media&token=1f9cee68-fb10-4cbf-b37d-8a93f077c5dc',1,@IdProperty)

SET @IdProperty=NEWID()
INSERT INTO Properties VALUES (@IdProperty,'Penthouse in COLLINS AVE','Reintroducing Le Penthouse at Chateau Beach Residences; this 2-story sky villa offers the utmost convenience. Private elevator entrance in both levels, soaring high ceilings with unobstructed views to the ocean & city skyline. Unit is appointed with book-matched marble throughout and top of the line stone and millwork. Custom made European kitchen with premium appliances, enclosed dining room. 9,050 SqFt of interior space & 4,523 SqFt of outdoor terraces with a grill; large see-through pool overlooking the Atlantic Ocean making a great entertainment experience. Primary Suite on the upper floor along with his/hers bathrooms and his/hers Ornare closets with 2 bonus rooms & Swedish Sauna; 3 suites on the Lower floor. Wine & cigar storage, bar, restaurant, Spa,Gym, beach towel services!','17475 COLLINS AVE #PH-3201,SUNNY ISLES BEACH,FL 33160',25.94,-80.12, 2023, 350000, 1, GETDATE(), GETDATE(), 1, 2, 1, 1, 1, 3, 2, 2700, 2, 1, 1, 1, 1, 5, 2,@IdZone2,@IdAccount3)
INSERT INTO PropertyImages VALUES 
(NEWID(),'https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Property%2FApt2-1.png?alt=media&token=5e30f1f6-1e7d-4f69-b3f6-fe04eb367e1a',1,@IdProperty),
(NEWID(),'https://firebasestorage.googleapis.com/v0/b/weelo-9c10a.appspot.com/o/Property%2FApt2-2.png?alt=media&token=b23463b5-5d30-40e7-8906-7a62674fdbed',1,@IdProperty)

INSERT INTO [Messages] VALUES
(1,1,'Successful response.'),
(2,2,'Oops, an error has occurred! Please contact our support team.'),
(3,2,'Does not exist.'),
(4,2,'Required.'),
(5,2,'It exceeds the allowed values.'),
(6,2,'The transaction was not processed.'),
(7,2,'It already exists.'),
(8,2,'The minimum value must be less than the greater.')
 
    COMMIT; 
END TRY
BEGIN CATCH
    ROLLBACK;
END CATCH;";

            migrationBuilder.Sql(script);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
