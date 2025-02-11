USE GadgetHub;
GO

SET IDENTITY_INSERT GadgetCatalog ON;

MERGE INTO GadgetCatalog AS target
USING (VALUES
    -- Smartphones
    (1, 'Smartphone X1', 'TechNova', 699.99, 'A high-performance smartphone.', 'Smartphones'),
    (2, 'Smartphone Y2', 'GadgetPro', 599.99, 'A budget-friendly smartphone.', 'Smartphones'),
    (3, 'Smartphone Z3', 'PhoneMaster', 499.99, 'A reliable smartphone with good features.', 'Smartphones'),
    (4, 'Smartphone A4', 'MobileHub', 649.99, 'Premium smartphone with 5G capability.', 'Smartphones'),
    (5, 'Smartphone B5', 'TechGiant', 799.99, 'Smartphone with amazing camera features.', 'Smartphones'),
    (6, 'Smartphone C6', 'GizmoTech', 549.99, 'Smartphone with extended battery life.', 'Smartphones'),
    (7, 'Smartphone D7', 'SpeedMobile', 399.99, 'Affordable smartphone with good performance.', 'Smartphones'),
    (8, 'Smartphone E8', 'QuickPhone', 449.99, 'Budget smartphone with solid performance.', 'Smartphones'),
    (9, 'Smartphone F9', 'ConnectMobile', 599.99, 'Mid-range smartphone with high display quality.', 'Smartphones'),
    (10, 'Smartphone G10', 'PhoneLife', 699.99, 'Smartphone with long-lasting battery and powerful specs.', 'Smartphones'),

    -- Tablets
    (11, 'Tablet Z3', 'ElectroMax', 499.99, 'A lightweight tablet.', 'Tablets'),
    (12, 'Tablet Q4', 'TechSmith', 329.99, 'Compact tablet for media consumption.', 'Tablets'),
    (13, 'Tablet W5', 'DevicePro', 399.99, 'Tablet with pen support for creative tasks.', 'Tablets'),
    (14, 'Tablet R6', 'TechNova', 349.99, 'Affordable tablet with good display.', 'Tablets'),
    (15, 'Tablet S7', 'GizmoHub', 499.99, 'Tablet with high resolution and long battery life.', 'Tablets'),
    (16, 'Tablet T8', 'FlexTech', 549.99, 'Tablet for professional use with keyboard support.', 'Tablets'),
    (17, 'Tablet U9', 'QuickTab', 259.99, 'Entry-level tablet with decent performance.', 'Tablets'),
    (18, 'Tablet V10', 'MobileEdge', 329.99, 'Tablet with high-quality screen for media use.', 'Tablets'),
    (19, 'Tablet X11', 'SmartTech', 549.99, 'Advanced tablet for gaming and streaming.', 'Tablets'),
    (20, 'Tablet Y12', 'PowerTab', 599.99, 'Tablet with great multitasking capabilities.', 'Tablets'),

    -- Laptops
    (21, 'Laptop B2', 'CompuWorld', 999.99, 'A powerful laptop for work and gaming.', 'Laptops'),
    (22, 'Laptop C3', 'TechWorks', 799.99, 'Lightweight laptop for everyday tasks.', 'Laptops'),
    (23, 'Laptop D4', 'SpeedTech', 1199.99, 'Gaming laptop with high-end specs and display.', 'Laptops'),
    (24, 'Laptop E5', 'LaptopLab', 899.99, 'Laptop with excellent battery life and solid performance.', 'Laptops'),
    (25, 'Laptop F6', 'TechMakers', 1099.99, 'Laptop with 16GB RAM and fast processing speed.', 'Laptops'),
    (26, 'Laptop G7', 'CompuExperts', 949.99, 'Mid-range laptop for casual gaming and work.', 'Laptops'),
    (27, 'Laptop H8', 'DevicePro', 799.99, 'Laptop with a good balance of performance and price.', 'Laptops'),
    (28, 'Laptop I9', 'WorkTech', 1199.99, 'Business laptop with high security features.', 'Laptops'),
    (29, 'Laptop J10', 'GadgetForce', 1299.99, 'Laptop with premium features and design.', 'Laptops'),
    (30, 'Laptop K11', 'TechPioneers', 899.99, 'Affordable laptop with good performance for students.', 'Laptops'),

    -- Wearables
    (31, 'Smartwatch A1', 'TimeTech', 199.99, 'A stylish smartwatch.', 'Wearables'),
    (32, 'Fitness Tracker B2', 'FitTrack', 129.99, 'A wearable fitness tracker with heart rate monitoring.', 'Wearables'),
    (33, 'VR Headset C3', 'ImmersionTech', 499.99, 'Virtual reality headset.', 'Wearables'),
    (34, 'Smart Band D4', 'SmartLife', 89.99, 'Fitness band with sleep tracking and heart rate monitor.', 'Wearables'),
    (35, 'Smartwatch E5', 'PowerWear', 249.99, 'Smartwatch with notifications and fitness tracking.', 'Wearables'),
    (36, 'Fitness Watch F6', 'HealthSync', 179.99, 'Health-focused smartwatch with workout tracking.', 'Wearables'),
    (37, 'GPS Tracker G7', 'WayTracker', 149.99, 'Wearable GPS tracker for fitness and outdoor activities.', 'Wearables'),
    (38, 'Smart Glasses H8', 'VisionTech', 299.99, 'Smart glasses with augmented reality features.', 'Wearables'),
    (39, 'Sleep Tracker I9', 'GoodSleep', 129.99, 'Wearable device that tracks your sleep quality.', 'Wearables'),
    (40, 'Smartwatch J10', 'TechTime', 179.99, 'Smartwatch with step counting and heart rate monitor.', 'Wearables')

) AS Source (Id, [Name], Brand, Price, [Description], Category)
    ON target.Id = Source.Id
WHEN MATCHED THEN 
    UPDATE SET target.[Name] = Source.[Name], target.Brand = Source.Brand, target.Price = Source.Price, target.[Description] = Source.[Description], target.Category = Source.Category
WHEN NOT MATCHED THEN 
    INSERT (Id, [Name], Brand, Price, [Description], Category) 
    VALUES (Source.Id, Source.[Name], Source.Brand, Source.Price, Source.[Description], Source.Category);

SET IDENTITY_INSERT GadgetCatalog OFF;
