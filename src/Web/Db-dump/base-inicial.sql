-- =============================================
-- Datos de prueba - Proyecto PNT1
-- =============================================

-- Limpiar tablas en orden (hijos antes que padres)
DELETE FROM PedidosProductos;
DELETE FROM CarritosProductos;
DELETE FROM Pedidos;
DELETE FROM Carritos;
DELETE FROM Productos;
DELETE FROM Categorias;

-- Resetear identidades para que los IDs empiecen en 1
DBCC CHECKIDENT ('Categorias', RESEED, 0);
DBCC CHECKIDENT ('Productos', RESEED, 0);

-- Categorias (IDs: 1=Electronica, 2=Indumentaria, 3=Alimentos y Bebidas, 4=Hogar y Decoracion, 5=Deportes)
INSERT INTO Categorias (Nombre, Descripcion, Activa) VALUES ('Electronica', 'Smartphones, laptops, TV y accesorios tecnologicos', 1);
INSERT INTO Categorias (Nombre, Descripcion, Activa) VALUES ('Indumentaria', 'Ropa, calzado y accesorios de moda', 1);
INSERT INTO Categorias (Nombre, Descripcion, Activa) VALUES ('Alimentos y Bebidas', 'Productos alimenticios, bebidas e infusiones', 1);
INSERT INTO Categorias (Nombre, Descripcion, Activa) VALUES ('Hogar y Decoracion', 'Muebles, electrodomesticos y articulos para el hogar', 1);
INSERT INTO Categorias (Nombre, Descripcion, Activa) VALUES ('Deportes', 'Ropa deportiva, calzado y equipamiento', 1);

-- Productos
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (1, 'iPhone 15 128GB', 'ELEC-001', 'Smartphone Apple iPhone 15, pantalla 6.1 pulgadas, chip A16 Bionic, camara 48MP, color negro', 1199999, '/img/productos/iphone15.jpg', 1, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (1, 'Samsung Galaxy S24 256GB', 'ELEC-002', 'Smartphone Samsung Galaxy S24, pantalla 6.2 pulgadas AMOLED, Snapdragon 8 Gen 3, camara 50MP', 899999, '/img/productos/galaxy-s24.jpg', 1, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (1, 'Smart TV LG 55 4K UHD', 'ELEC-003', 'Televisor LG 55 pulgadas, resolucion 4K, WebOS, HDR10, compatible con Alexa y Google Assistant', 749999, '/img/productos/tv-lg-55.jpg', 1, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (1, 'Notebook Lenovo IdeaPad 3', 'ELEC-004', 'Notebook 15.6 pulgadas, Intel Core i5, 8GB RAM, 512GB SSD, Windows 11 Home', 649999, '/img/productos/lenovo-ideapad.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (1, 'Auriculares JBL Tune 770NC', 'ELEC-005', 'Auriculares inalambricos con cancelacion de ruido, 70 horas de bateria, Bluetooth 5.3', 89999, '/img/productos/jbl-770nc.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (1, 'Tablet Samsung Galaxy Tab A9', 'ELEC-006', 'Tablet 8.7 pulgadas, 4GB RAM, 64GB, Wi-Fi, pantalla LCD TFT, ideal para estudio y entretenimiento', 329999, '/img/productos/tab-a9.jpg', 0, 1, GETDATE(), GETDATE());

INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (2, 'Remera Nike Dri-FIT', 'IND-001', 'Remera deportiva unisex de secado rapido, tejido Dri-FIT, talle M', 24999, '/img/productos/remera-nike.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (2, 'Zapatillas Adidas Ultraboost', 'IND-002', 'Zapatillas running con tecnologia Boost, suela Continental, talle 42', 94999, '/img/productos/ultraboost.jpg', 1, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (2, 'Campera The North Face', 'IND-003', 'Campera cortaviento impermeable, relleno de plumas sinteticas, talle L', 189999, '/img/productos/tnf-campera.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (2, 'Jeans Levis 501 Original', 'IND-004', 'Jean de corte recto clasico, tela 100 por ciento algodon, color azul oscuro, talle 32x30', 74999, '/img/productos/levis-501.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (2, 'Buzo con Capucha Puma', 'IND-005', 'Buzo canguro unisex de algodon premium, logo bordado, color gris melange, talle L', 44999, '/img/productos/buzo-puma.jpg', 0, 1, GETDATE(), GETDATE());

INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (3, 'Yerba Mate Taragui 1kg', 'ALI-001', 'Yerba mate elaborada seleccionada, sabor intenso con palo, presentacion 1 kilogramo', 6499, '/img/productos/taragui-1kg.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (3, 'Alfajores Havanna x6', 'ALI-002', 'Caja de 6 alfajores de chocolate negro rellenos de dulce de leche, sabor tradicional', 18999, '/img/productos/havanna-x6.jpg', 1, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (3, 'Dulce de Leche La Serenisima', 'ALI-003', 'Dulce de leche familiar repostero, pote de 400 gramos, ideal para facturas y tortas', 4299, '/img/productos/ddl-serenisima.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (3, 'Vino Malbec Trapiche Reserva', 'ALI-004', 'Vino tinto Malbec Reserva, cosecha 2022, Mendoza, botella 750ml, medalla de oro', 12499, '/img/productos/malbec-trapiche.jpg', 1, 1, GETDATE(), GETDATE());

INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (4, 'Set Mate y Bombilla Premium', 'HOG-001', 'Mate de calabaza curado con bombilla de acero inoxidable, incluye funda de cuero', 14999, '/img/productos/set-mate.jpg', 1, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (4, 'Cafetera Nespresso Essenza', 'HOG-002', 'Cafetera de capsulas compacta, presion 19 bares, deposito 1 litro, color negro', 144999, '/img/productos/nespresso.jpg', 0, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (4, 'Silla Gamer ThunderX3 TC3', 'HOG-003', 'Silla ergonomica con soporte lumbar, reposabrazos 4D, tapizado de cuero PU, color negro y rojo', 319999, '/img/productos/silla-gamer.jpg', 0, 1, GETDATE(), GETDATE());

INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (5, 'Camiseta Seleccion Argentina', 'DEP-001', 'Camiseta oficial Adidas de la Seleccion Argentina, temporada 2024, talle M, tres estrellas', 69999, '/img/productos/camiseta-arg.jpg', 1, 1, GETDATE(), GETDATE());
INSERT INTO Productos (CategoriaId, Nombre, SKU, Descripcion, Precio, UrlImagen, Destacado, Estado, FechaCreacion, FechaActualizacion) VALUES (5, 'Pelota Adidas Tango Glider', 'DEP-002', 'Pelota de futbol de entrenamiento, cubierta de TPU, talla 5, resistente a la abrasion', 34999, '/img/productos/pelota-adidas.jpg', 0, 1, GETDATE(), GETDATE());
