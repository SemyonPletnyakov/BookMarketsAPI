BEGIN;

CREATE TABLE addresses(
    address_id SERIAL PRIMARY KEY,
    country VARCHAR NOT NULL,
    region_number INTEGER NOT NULL,
    region_name VARCHAR NOT NULL,
    city VARCHAR NOT NULL,
    district VARCHAR,
    street VARCHAR NOT NULL,
    house VARCHAR NOT NULL,
    room VARCHAR NOT NULL --уникальность на комбинацию значений
);

CREATE TABLE shops(
	shop_id SERIAL PRIMARY KEY,
	name VARCHAR,
	opening_time TIME,
	closing_time TIME,
	address_id INTEGER REFERENCES addresses (address_id) NOT NULL
);

CREATE TABLE warehouses(
	warehouse_id SERIAL PRIMARY KEY,
	name VARCHAR,
	opening_time TIME,
	closing_time TIME,
	address_id INTEGER REFERENCES addresses (address_id) NOT NULL
);

CREATE TABLE products(
	product_id SERIAL PRIMARY KEY,
	name VARCHAR NOT NULL,
	description VARCHAR,
	price DECIMAL CHECK (price >= 0) NOT NULL,
    key_words VARCHAR[] --имеет смысл добавить индекс
);

CREATE TABLE authors(
	author_id SERIAL PRIMARY KEY,
    last_name VARCHAR NOT NULL,
	first_name VARCHAR NOT NULL,
    patronymic VARCHAR,
    birth_date DATE,
    countries VARCHAR[]
);

CREATE TABLE books(
    product_id INTEGER REFERENCES products (product_id) ON DELETE CASCADE NOT NULL,
	author_id INTEGER REFERENCES authors (author_id),

    PRIMARY KEY(product_id)
);

CREATE TABLE products_in_shops(
    product_id INTEGER REFERENCES products (product_id) NOT NULL,
    shop_id INTEGER REFERENCES shops (shop_id) NOT NULL,
    count INTEGER CHECK(count >= 0) NOT NULL,
    
    PRIMARY KEY(product_id, shop_id)
);

CREATE TABLE products_in_warehouses(
    product_id INTEGER REFERENCES products (product_id) NOT NULL,
    warehouse_id INTEGER REFERENCES warehouses (warehouse_id) NOT NULL,
    count INTEGER CHECK(count >= 0) NOT NULL,

    PRIMARY KEY(product_id, warehouse_id)
);

CREATE TABLE employees(
	employee_id SERIAL PRIMARY KEY,
    last_name VARCHAR NOT NULL,
	first_name VARCHAR NOT NULL,
    patronymic VARCHAR,
    birth_date DATE,
    phone VARCHAR,
    email VARCHAR,
    job_title VARCHAR NOT NULL,
    login VARCHAR UNIQUE NOT NULL,
    password VARCHAR NOT NULL
);

CREATE TABLE links_employees_and_shops(
    employee_id INTEGER REFERENCES employees (employee_id) NOT NULL,
    shop_id INTEGER REFERENCES shops (shop_id) NOT NULL
);

CREATE TABLE links_employees_and_warehouses(
    employee_id INTEGER REFERENCES employees (employee_id) NOT NULL,
    warehouse_id INTEGER REFERENCES warehouses (warehouse_id) NOT NULL
);

CREATE TABLE customers(
    customer_id SERIAL PRIMARY KEY,
    last_name VARCHAR,
	first_name VARCHAR,
    patronymic VARCHAR,
    birth_date DATE,
    phone VARCHAR,
    email VARCHAR UNIQUE NOT NULL,
    password VARCHAR
);

CREATE TABLE orders(
    order_id SERIAL PRIMARY KEY,
    customer_id INTEGER REFERENCES customers (customer_id) NOT NULL,
    shop_id INTEGER REFERENCES shops (shop_id) NOT NULL,
    date TIMESTAMP WITH TIME ZONE NOT NULL,
    status INTEGER NOT NULL
);

CREATE TABLE products_in_orders(
    order_id INTEGER REFERENCES orders (order_id) NOT NULL,
    product_id INTEGER REFERENCES products (product_id) NOT NULL,
    count INTEGER CHECK(count >= 0) NOT NULL,
    actual_price DECIMAL CHECK (actual_price >= 0) NOT NULL,

    PRIMARY KEY(order_id, product_id)
);

COMMIT;