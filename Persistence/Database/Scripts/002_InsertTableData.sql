INSERT INTO "Products" ("Id", "Name", "Description", "Category", "AvailableQuantity", "WarehouseLocation", "EntryDate", "ExpirationDate")
VALUES
    ('550e8400-e29b-41d4-a716-446655440001', 'Paracetamol 500mg', 'Pain reliever and fever reducer', 'Pain Relief', 100, 'A1-01', 1708670400, 1730198400),
    ('550e8400-e29b-41d4-a716-446655440002', 'Ibuprofen 400mg', 'Anti-inflammatory and pain reliever', 'Pain Relief', 150, 'A1-02', 1708584000, 1727745600),
    ('550e8400-e29b-41d4-a716-446655440003', 'Cetirizine 10mg', 'Antihistamine for allergies', 'Allergy Relief', 80, 'B2-05', 1708497600, 1725254400),
    ('550e8400-e29b-41d4-a716-446655440004', 'Omeprazole 20mg', 'Reduces stomach acid production', 'Gastrointestinal', 120, 'C3-10', 1708411200, 1732680000),
    ('550e8400-e29b-41d4-a716-446655440005', 'Salbutamol Inhaler', 'Relieves bronchospasm in asthma', 'Respiratory', 50, 'D4-03', 1708324800, 1722748800);

INSERT INTO "InventoryMovements" ("Id", "ProductId", "MovementType", "Quantity", "MovementDate", "Origin", "Destination")
VALUES
    ('660e8400-e29b-41d4-a716-446655440001', '550e8400-e29b-41d4-a716-446655440001', 'IN', 100, 1708670500, 'Supplier', 'Main Warehouse'),
    ('660e8400-e29b-41d4-a716-446655440002', '550e8400-e29b-41d4-a716-446655440002', 'OUT', 20, 1708670600, 'Main Warehouse', 'Branch 1'),
    ('660e8400-e29b-41d4-a716-446655440003', '550e8400-e29b-41d4-a716-446655440003', 'IN', 80, 1708670700, 'Supplier', 'Main Warehouse'),
    ('660e8400-e29b-41d4-a716-446655440004', '550e8400-e29b-41d4-a716-446655440004', 'OUT', 15, 1708670800, 'Main Warehouse', 'Branch 2'),
    ('660e8400-e29b-41d4-a716-446655440005', '550e8400-e29b-41d4-a716-446655440005', 'OUT', 10, 1708670900, 'Main Warehouse', 'Branch 3');
