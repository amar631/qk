enum PaymentType {
    creditCard = 101,
    debitCard = 102,
    netBanking = 103,
    cash = 104,
}

class Transaction {
    constructor(
        public userTransactionId: string,
        public emailId: string,
        public amount: number,
        public transactionDateTime: Date,
        public paymentTypeId: PaymentType,
        public status: string
    ) { }
}

class UserTransactions {
    private listOfUserTransactions: Transaction[];

    constructor(listOfUserTransactions: Transaction[]) {
        this.listOfUserTransactions = listOfUserTransactions;
    }

    public getUserTransactions(emailId: string): Transaction[] {
        const filteredTransactions: Transaction[] = [];

        for (const transaction of this.listOfUserTransactions) {
            if (transaction.emailId === emailId) {
                filteredTransactions.push(transaction);
            }
        }

        return filteredTransactions;
    }
}

// Sample usage (assuming some transactions exist)
const transactions = [
    new Transaction(
        "TX123",
        "test@example.com",
        500,
        new Date("2024-06-12"),
        PaymentType.debitCard,
        "Success"
    ),
    new Transaction(
        "TX456",
        "john.doe@example.com",
        1234.56,
        new Date("2024-05-15"),
        PaymentType.netBanking,
        "Pending"
    ),
    new Transaction(
        "TX123",
        "test@example.com",
        12300,
        new Date("2022-12-01"),
        PaymentType.debitCard,
        "Success"
    ),
    // if needed add more transactions here
];

const userTransactions = new UserTransactions(transactions);
const userTransactionsForUser = userTransactions.getUserTransactions("test@example.com");

console.log("Transactions for email: test@example.com");
if (userTransactionsForUser.length > 0) {
    for (const transaction of userTransactionsForUser) {
        console.log("Transaction Details:");
        /*console.log("  userTransactionId:", transaction.userTransactionId);
        console.log("  emailId:", transaction.emailId);*/
        console.log("  amount:", transaction.amount);
        console.log("  transactionDateTime:", transaction.transactionDateTime.toLocaleDateString());
        /*console.log("  paymentTypeId:", transaction.paymentTypeId);
        console.log("  status:", transaction.status);*/
        console.log("-----------------------------------");
    }
} else {
    console.log("No transactions found for this email.");
}
