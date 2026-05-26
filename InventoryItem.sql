CREATE TABLE dbo.InventoryItem
(
    Oid INT IDENTITY(1,1) NOT NULL PRIMARY KEY,

    ItemNumber NVARCHAR(50) NULL,
    ItemName NVARCHAR(150) NULL,
    Category NVARCHAR(100) NULL,
    QuantityOnHand INT NOT NULL DEFAULT 0,
    UnitCost DECIMAL(18,2) NOT NULL DEFAULT 0
);

INSERT INTO dbo.InventoryItem
(
    ItemNumber,
    ItemName,
    Category,
    QuantityOnHand,
    UnitCost
)
VALUES
('INV-1001', 'Printer Paper 8.5x11', 'Office Supplies', 250, 6.99),
('INV-1002', 'Black Ink Cartridge', 'Office Supplies', 45, 24.95),
('INV-1003', 'Blue Ballpoint Pens', 'Office Supplies', 300, 0.35),
('INV-1004', 'File Folders Letter Size', 'Office Supplies', 180, 0.22),
('INV-1005', 'Stapler Standard', 'Office Supplies', 32, 7.50),
('INV-1006', 'Staples Box', 'Office Supplies', 120, 1.25),
('INV-1007', 'Desk Calculator', 'Office Supplies', 18, 12.99),
('INV-1008', 'Shipping Labels', 'Office Supplies', 95, 4.75),
('INV-1009', 'Packing Tape Roll', 'Shipping', 140, 3.25),
('INV-1010', 'Bubble Wrap Roll', 'Shipping', 35, 18.50),

('INV-1011', 'Corrugated Box Small', 'Shipping', 220, 0.85),
('INV-1012', 'Corrugated Box Medium', 'Shipping', 175, 1.25),
('INV-1013', 'Corrugated Box Large', 'Shipping', 90, 2.10),
('INV-1014', 'Padded Mailer Small', 'Shipping', 260, 0.55),
('INV-1015', 'Padded Mailer Large', 'Shipping', 130, 0.95),
('INV-1016', 'Barcode Labels', 'Warehouse', 500, 0.08),
('INV-1017', 'Barcode Scanner USB', 'Warehouse', 12, 89.99),
('INV-1018', 'Inventory Tags', 'Warehouse', 400, 0.12),
('INV-1019', 'Shelf Label Holders', 'Warehouse', 75, 1.15),
('INV-1020', 'Storage Bin Small', 'Warehouse', 65, 4.50),

('INV-1021', 'Storage Bin Medium', 'Warehouse', 48, 7.25),
('INV-1022', 'Storage Bin Large', 'Warehouse', 24, 13.95),
('INV-1023', 'Pallet Wrap Roll', 'Warehouse', 40, 19.99),
('INV-1024', 'Work Gloves Pair', 'Safety', 85, 5.75),
('INV-1025', 'Safety Glasses', 'Safety', 60, 3.95),
('INV-1026', 'Ear Protection', 'Safety', 35, 8.99),
('INV-1027', 'First Aid Kit', 'Safety', 10, 29.95),
('INV-1028', 'High Visibility Vest', 'Safety', 25, 12.50),
('INV-1029', 'Cleaning Wipes', 'Cleaning', 90, 4.25),
('INV-1030', 'Disinfectant Spray', 'Cleaning', 55, 6.75),

('INV-1031', 'Trash Bags 33 Gallon', 'Cleaning', 110, 9.99),
('INV-1032', 'Paper Towels', 'Cleaning', 150, 2.49),
('INV-1033', 'Hand Soap Refill', 'Cleaning', 70, 5.95),
('INV-1034', 'Floor Cleaner', 'Cleaning', 30, 11.25),
('INV-1035', 'Broom', 'Cleaning', 18, 8.50),
('INV-1036', 'Dustpan', 'Cleaning', 22, 3.75),
('INV-1037', 'Mop Head', 'Cleaning', 26, 6.25),
('INV-1038', 'Mop Handle', 'Cleaning', 15, 7.99),
('INV-1039', 'Computer Mouse USB', 'Technology', 28, 11.99),
('INV-1040', 'Keyboard USB', 'Technology', 20, 18.99),

('INV-1041', 'HDMI Cable 6ft', 'Technology', 42, 7.49),
('INV-1042', 'Ethernet Cable 10ft', 'Technology', 50, 4.99),
('INV-1043', 'USB Flash Drive 32GB', 'Technology', 33, 8.95),
('INV-1044', 'Monitor Stand', 'Technology', 14, 22.50),
('INV-1045', 'Surge Protector', 'Technology', 27, 16.75),
('INV-1046', 'Desk Chair Mat', 'Furniture', 8, 34.95),
('INV-1047', 'Folding Chair', 'Furniture', 16, 21.99),
('INV-1048', 'Folding Table', 'Furniture', 6, 64.50),
('INV-1049', 'Whiteboard Markers', 'Office Supplies', 80, 0.95),
('INV-1050', 'Dry Erase Board', 'Office Supplies', 9, 39.99);